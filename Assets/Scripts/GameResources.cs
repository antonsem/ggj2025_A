using UnityEngine;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance
    {
        get
        {
            _instance ??= Instance;
            return _instance;
        }
    }

    private static GameResources _instance;
    
    [SerializeField] private GameObject PlayerOne;
    [SerializeField] private GameObject PlayerTwo;

    public GameObject GetPlayer(PlayerType playerType)
    {
        return playerType == PlayerType.PlayerTwo ? PlayerTwo : PlayerOne;
    }
}
