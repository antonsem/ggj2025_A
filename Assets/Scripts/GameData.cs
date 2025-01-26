using System;
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
        private set
        {
        }
    }

    private static GameData _instance;

    public const int TotalFriends = 20; //TODO: Adjust the amount based on scene

    public event Action<PlayerType> OnGameWon;

    public int PlayerOneFriends { get; private set; } = 10;
    public int PlayerTwoFriends { get; private set; } = 5;

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
