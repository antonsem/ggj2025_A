using System;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public static GameResources Instance { get; private set; }

    [SerializeField] private GameObject PlayerOne;
    [SerializeField] private GameObject PlayerTwo;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public GameObject GetPlayer(PlayerType playerType)
    {
        return playerType == PlayerType.PlayerTwo ? PlayerTwo : PlayerOne;
    }

    private void OnDestroy()
    {
        GameData.Instance.Reset();
        Instance = null;
    }
}
