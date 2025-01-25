using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    private void Start()
    {
        FMODManager.Instance.PlaySound("event:/MX_MainTheme");
        InputManager.Instance.OnSchizoPressed += OnSchizo;
    }

    public void OnSchizo()
    {
        FMODManager.Instance.SetGlobalParameterValue("IsSchizo", 1);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnSchizoPressed -= OnSchizo;
    }
}
