using System;
using UnityEngine;

public class GameData
{
    public static GameData Instance
    {
        get
        {
            Instance ??= new GameData();
            return Instance;
        }
        private set
        {
        }
    }

    public const int TotalFriends = 20; //TODO: Adjust the amount based on scene

    public event Action<PlayerType> OnGameWon;
    
    public int PlayerOneFriends { get; private set; }
    public int PlayerTwoFriends { get; private set; }

    public void UpdateScore(PlayerType playerType, int difference)
    {
        switch(playerType)
        {
            case PlayerType.PlayerOne:
                PlayerOneFriends = Math.Clamp(PlayerOneFriends + difference, 0, TotalFriends);
                break;
            case PlayerType.PlayerTwo:
                PlayerTwoFriends += difference;
                break;
        }

        if(PlayerOneFriends + PlayerTwoFriends >= TotalFriends) //TODO: Or maybe change to number of friends bigger than a threshold?
        {
            OnGameWon?.Invoke(PlayerOneFriends > PlayerTwoFriends ? PlayerType.PlayerOne : PlayerType.PlayerTwo);
        }
    }
}
