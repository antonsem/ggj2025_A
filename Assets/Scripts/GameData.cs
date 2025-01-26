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

    public HashSet<GameObject> PlayerOneFriends { get; private set; }
    public HashSet<GameObject> PlayerTwoFriends { get; private set; }

    public void AssignFriend(GameObject friend, PlayerType playerType)
    {
        switch(playerType)
        {
            case PlayerType.PlayerOne:
                if(PlayerOneFriends.Add(friend))
                {
                    friend.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureOffset(_baseMap, new Vector2(0.66f,0));
                }
                PlayerTwoFriends.Remove(friend);
                OnScoreUpdated?.Invoke(playerType, PlayerOneFriends.Count);
                break;
            case PlayerType.PlayerTwo:
                if(PlayerTwoFriends.Add(friend))
                {
                    friend.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTextureOffset(_baseMap, new Vector2(0.33f, 0));
                }
                PlayerOneFriends.Remove(friend);
                OnScoreUpdated?.Invoke(playerType, PlayerTwoFriends.Count);
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
