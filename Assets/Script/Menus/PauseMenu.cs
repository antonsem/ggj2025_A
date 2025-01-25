using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private StarterAssetsInputs input;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool _gameIsPaused = false;

    private void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");
    }

    private void Update()
    {
        if (input.gameIsPaused && !_gameIsPaused)
        {
            _gameIsPaused = true;
            PauseGame();
        }
    }

    public void UpdateVolume()
    {
        FMODManager.Instance.SetVCAVolume("Music", musicSlider.value);
        FMODManager.Instance.SetVCAVolume("SFX", sfxSlider.value);

        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
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
        _gameIsPaused = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
