using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private StarterAssetsInputs input;

    private bool gameIsPaused = false;

    private void Update()
    {
        if (input.gameIsPaused && !gameIsPaused)
        {
            gameIsPaused = true;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        // Lock Time
        Time.timeScale = 0f;

        // Unlock Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pauseMenu.gameObject.SetActive(true);
    }

    public void Resume()
    {
        // Unlock time
        Time.timeScale = 1f;

        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        input.gameIsPaused = false;

        pauseMenu.gameObject.SetActive(false);
        gameIsPaused = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
