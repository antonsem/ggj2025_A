using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StarterAssetsInputs), typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public event Action OnSchizoPressed;
    public event Action OnPlayerTwoJoined;
    
    public static InputManager Instance { get; private set; }

    [HideInInspector] public StarterAssetsInputs StarterAssetsInputsPlayerOne;
    [HideInInspector] public StarterAssetsInputs StarterAssetsInputsPlayerTwo;
    [HideInInspector] public PlayerInput PlayerInputPlayerOne;
    [HideInInspector] public PlayerInput PlayerInputPlayerTwo;
    [HideInInspector] public bool PlayerTwoJoined;

    [SerializeField] private PlayerInput _playerTwoInput;
    
    private int _numberOfPlayers;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        PlayerInputPlayerOne = GetComponent<PlayerInput>();
        StarterAssetsInputsPlayerOne = GetComponent<StarterAssetsInputs>();
        PlayerInputPlayerOne.enabled = true;
        
        PlayerInputPlayerTwo = _playerTwoInput;
        StarterAssetsInputsPlayerTwo = _playerTwoInput.GetComponent<StarterAssetsInputs>();
        PlayerInputPlayerTwo.enabled = _numberOfPlayers >= 2;
    }

    public void OnSchizo(InputValue value)
    {
        OnSchizoPressed?.Invoke();
    }

    public void OnPlayerJoined(PlayerInput value)
    {
        if(_numberOfPlayers == 0)
        {
            GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            _playerTwoInput.enabled = true;
            PlayerTwoJoined = true;
        }

        ++_numberOfPlayers;
    }
}
