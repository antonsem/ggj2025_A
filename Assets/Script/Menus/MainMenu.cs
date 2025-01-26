using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Button References")]
    public GameObject[] buttons;

    [Header("Floating Effect Settings")]
    public float amplitude = 5f;
    public float speed = 0.01f;

    private Vector2[] initialPositions;

    private void Start()
    {
        initialPositions = new Vector2[buttons.Length];
        
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rt = buttons[i].GetComponent<RectTransform>();
            if (rt != null)
            {
                initialPositions[i] = rt.anchoredPosition;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rt = buttons[i].GetComponent<RectTransform>();

            if (rt == null)
            {
                continue;
            }

            float x = amplitude * Mathf.Sin((speed * Time.time) + i);
            float y = amplitude * Mathf.Sin(2f * (speed * Time.time) + i);

            rt.anchoredPosition = initialPositions[i] + new Vector2(x, y);
        }
    }

    public void PlayGame1P()
    {
        FMODManager.Instance.PlaySound("event:/MX_MainTheme");
        GameModeManager.Instance.SetSinglePlayer();

        SceneManager.LoadSceneAsync(1);
    }

    public void PlayGame2P()
    {
        FMODManager.Instance.PlaySound("event:/MX_MainTheme");
        GameModeManager.Instance.SetMultiplayer();
        
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
