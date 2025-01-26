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

    public const int TotalFriends = 10;

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
                    
                    switch (PlayerOneFriends.Count)
                    {
                        case > 8:
                            FMODManager.SetGlobalParameterString("Bass_Speed", "Bass_High");
                            break;
                        case < 3:
                            FMODManager.SetGlobalParameterString("Bass_Speed", "Bass_Slow");
                            break;
                        default:
                            FMODManager.SetGlobalParameterString("Bass_Speed", "Bass_Med");
                            break;
                    }
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
                    

                    switch (PlayerTwoFriends.Count)
                    {
                        case > 8:
                            FMODManager.SetGlobalParameterString("Drum_Speed", "Drum_High");
                            break;
                        case < 3:
                            FMODManager.SetGlobalParameterString("Drum_Speed", "Drum_Low");
                            break;
                        default:
                            FMODManager.SetGlobalParameterString("Drum_Speed", "Drum_Med");
                            break;
                    }
                }
                if(PlayerOneFriends.Remove(friend))
                {
                    OnScoreUpdated?.Invoke(PlayerType.PlayerOne, PlayerOneFriends.Count);
                }
                break;
        }

        if(PlayerOneFriends.Count >= TotalFriends)
        {
            OnGameWon?.Invoke(PlayerType.PlayerOne);
        }
        else if(PlayerTwoFriends.Count >= TotalFriends)
        {
            OnGameWon?.Invoke(PlayerType.PlayerTwo);
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

    public void Reset()
    {
        PlayerOneFriends.Clear();
        PlayerTwoFriends.Clear();
    }
}
