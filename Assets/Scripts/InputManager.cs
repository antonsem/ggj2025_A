using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StarterAssetsInputs), typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public event Action OnSchizoPressed;
    
    public static InputManager Instance { get; private set; }

    [HideInInspector] public StarterAssetsInputs StarterAssetsInputsPlayerOne;
    [HideInInspector] public StarterAssetsInputs StarterAssetsInputsPlayerTwo;
    [HideInInspector] public PlayerInput PlayerInputPlayerOne;
    [HideInInspector] public PlayerInput PlayerInputPlayerTwo;

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
        Debug.Log("HELLO");
        if(_numberOfPlayers == 0)
        {
            GetComponent<PlayerInput>().enabled = true;
        }
        else
        {
            // _playerTwoInput.actions = GetComponent<PlayerInput>().actions;
            // _playerTwoInput.defaultControlScheme = "Gamepad";
            // _playerTwoInput.neverAutoSwitchControlSchemes = true;
            _playerTwoInput.enabled = true;
        }

        ++_numberOfPlayers;
    }
}
