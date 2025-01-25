using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StarterAssetsInputs), typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public event Action OnSchizoPressed;
    
    public static InputManager Instance { get; private set; }

    public StarterAssetsInputs StarterAssetsInputs;
    public PlayerInput PlayerInput;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    public void OnSchizo(InputValue value)
    {
        OnSchizoPressed?.Invoke();
    }
}
