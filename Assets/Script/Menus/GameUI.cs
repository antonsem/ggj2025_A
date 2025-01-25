using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject[] iconPrefab;
    [SerializeField] private GameObject[] iconContainer;
    [SerializeField] private TMP_Text friendCountText;

    [SerializeField] private GameObject player;

    [SerializeField] private int _friendCount = 10;

    private void Update()
    {
        UpdateBubbleCounter();

        UpdateFriendCount();
        UpdateIconContainer();


    }

    private void UpdateBubbleCounter()
    {
        // Get Bubble Counter from player controller
    }

    private void UpdateFriendCount()
    {
        // friendCount = player.friends;

        friendCountText.text = _friendCount.ToString();
    }

    private void UpdateIconContainer()
    {

        for (int i = 0; i < iconContainer.Length; i++)
        {
            if (i < _friendCount)
            {
                iconContainer[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                iconContainer[i].GetComponent<Image>().enabled = false;
            }
        }
    }

}
