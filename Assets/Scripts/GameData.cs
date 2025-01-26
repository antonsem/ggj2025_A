using System;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public static GameData Instance
    {
        get
        {
            _instance ??= new GameData();
            return _instance;
        }
    }

    private static GameData _instance;
    
    private static readonly int _baseMap = Shader.PropertyToID("_BaseMap");

    public const int TotalFriends = 20; //TODO: Adjust the amount based on scene

    public event Action<PlayerType> OnGameWon;
    public event Action<PlayerType, int> OnScoreUpdated;

    public HashSet<GameObject> PlayerOneFriends { get; } = new();
    public HashSet<GameObject> PlayerTwoFriends { get; } = new();

    public void AssignFriend(GameObject friend, PlayerType playerType)
    {
        switch(playerType)
        {
            case PlayerType.PlayerOne:
                if(PlayerOneFriends.Add(friend))
                {
                    friend.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureOffset(_baseMap, new Vector2(0.66f,0));
                    OnScoreUpdated?.Invoke(playerType, PlayerOneFriends.Count);
                }
                if(PlayerTwoFriends.Remove(friend))
                {
                    OnScoreUpdated?.Invoke(PlayerType.PlayerTwo, PlayerTwoFriends.Count);
                }
                break;
            case PlayerType.PlayerTwo:
                if(PlayerTwoFriends.Add(friend))
                {
                    friend.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureOffset(_baseMap, new Vector2(0.33f, 0));
                    OnScoreUpdated?.Invoke(playerType, PlayerTwoFriends.Count);
                }
                if(PlayerOneFriends.Remove(friend))
                {
                    OnScoreUpdated?.Invoke(PlayerType.PlayerOne, PlayerOneFriends.Count);
                }
                break;
        }

        if(PlayerOneFriends.Count + PlayerTwoFriends.Count >= TotalFriends) //TODO: Or maybe change to number of friends bigger than a threshold?
        {
            OnGameWon?.Invoke(PlayerOneFriends.Count > PlayerTwoFriends.Count ? PlayerType.PlayerOne : PlayerType.PlayerTwo);
        }
    }

    public bool IsAlreadyMyFriend(GameObject friend, PlayerType playerType)
    {
        return playerType switch
        {
            PlayerType.PlayerOne => PlayerOneFriends.Contains(friend),
            PlayerType.PlayerTwo => PlayerTwoFriends.Contains(friend),
            _ => false
        };
    }
}
