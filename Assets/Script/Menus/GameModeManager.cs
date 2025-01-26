using UnityEngine;

public class GameModeManager
{
    public static GameModeManager Instance
    {
        get
        {
            _instance ??= new GameModeManager();
            return _instance;
        }
        private set
        {
        }
    }


    public enum GameMode { SinglePlayer, MultiPlayer }
    public GameMode gameMode { get; private set; }

    private static GameModeManager _instance;

    public void SetSinglePlayer()
    {
        gameMode = GameMode.SinglePlayer;
    }

    public void SetMultiplayer()
    {
        gameMode = GameMode.MultiPlayer;
    }


}
