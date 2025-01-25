using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event Action OnSchizoPressed;
    
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void OnSchizo(InputValue value)
    {
        OnSchizoPressed?.Invoke();
    }
}
