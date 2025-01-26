using UnityEngine;
using StarterAssets;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject playerOneVictory;
    [SerializeField] private GameObject playerTwoVictory;

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool _gameIsPaused = false;
    private float? _gameWon;

    private void Awake()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");

        GameData.Instance.OnGameWon += GameWon;
    }

    private void GameWon(PlayerType playerType)
    {
        switch(playerType)
        {
            case PlayerType.PlayerOne:
                playerOneVictory.SetActive(true);
                _gameWon = Time.time;
                break;
            case PlayerType.PlayerTwo:
                playerTwoVictory.SetActive(true);
                _gameWon = Time.time;
                break;
        }
    }

    private void Update()
    {
        if(_gameWon.HasValue)
        {
            if(Time.time - _gameWon.Value > 2f)
            {
                BackToMainMenu();
            }
        }
        
        if (AnyPlayerPaused() && !_gameIsPaused)
        {
            _gameIsPaused = true;
            PauseGame();
        }
    }
    
    private bool AnyPlayerPaused() => InputManager.Instance.StarterAssetsInputsPlayerOne.gameIsPaused ||
        InputManager.Instance.StarterAssetsInputsPlayerTwo.gameIsPaused;

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

        InputManager.Instance.StarterAssetsInputsPlayerOne.gameIsPaused = false;
        InputManager.Instance.StarterAssetsInputsPlayerTwo.gameIsPaused = false;

        pauseMenu.gameObject.SetActive(false);
        _gameIsPaused = false;
    }

    public void ToMainMenu()
    {
        Resume();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameData.Instance.OnGameWon -= GameWon;
        
        SceneManager.LoadSceneAsync(0);
    }

    public void BackToMainMenu()
    {
        Resume();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        SceneManager.LoadSceneAsync(0);
    }

    private void OnDestroy()
    {
        GameData.Instance.OnGameWon -= GameWon;
    }
}
