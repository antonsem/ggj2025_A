using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject UIPanel;

    [Header("Friend Counter")]
    [SerializeField] private GameObject[] iconContainer;
    [SerializeField] private TMP_Text friendCountText;

    [Header("Bubble Bar")]
    [SerializeField] private Slider bubbleSlider;
    [SerializeField] private TMP_Text keysText;

    [Header("Player Selection")]
    [SerializeField] private PlayerType playerType;

    private void Start()
    {
        if (playerType == PlayerType.PlayerOne)
        {
            UIPanel.SetActive(true);
        }
        else if (playerType == PlayerType.PlayerTwo && GameModeManager.Instance.gameMode == GameModeManager.GameMode.MultiPlayer)
        {
            UIPanel.SetActive(true);
        }
        else
        {
            UIPanel.SetActive(false);
        }

        GameData.Instance.OnScoreUpdated += UpdateFriendCount;
    }

    private void Awake()
    {

    }

    public void UpdateBubbleCounter(float bubbleValue)
    {
        bubbleSlider.value = bubbleValue;

        if (bubbleValue > 0)
        {
            keysText.text = "Press [Space] to Pop Bubbles";
        }
        else
        {
            keysText.text = "Press [E] to build pressure";
        }

    }

    private void UpdateFriendCount(PlayerType player, int friendCount)
    {
        if(playerType == player)
        {
            friendCountText.text = friendCount.ToString();
            UpdateIconContainer(iconContainer, friendCount);
        }
    }

    private void UpdateIconContainer(GameObject[] iconContainer, int friendCount)
    {
        for (int i = 0; i < iconContainer.Length; i++)
        {
            if (i < friendCount)
            {
                iconContainer[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                iconContainer[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        GameData.Instance.OnScoreUpdated -= UpdateFriendCount;
    }
}
