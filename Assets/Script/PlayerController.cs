using System;
using System.Runtime.Serialization;
using FMOD.Studio;
using StarterAssets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Bubble Settings")]

    
    [SerializeField] public float animationLengthSeconds = 0.3f;
    [SerializeField] private float fastPressBonusSeconds = 1f;
    [SerializeField] public float maxTimeHoldingBubblesSeconds = 2f;
    [SerializeField] public float bubbleDecayOvertime = 0.01f;
    [SerializeField] public float releaseForceMultiplier = 15f;
    [SerializeField] private bool _isPlayerOne;
    [SerializeField] public GameUI gameUI;

    // Current bubble count
    private float _currentBubbles = 0f;

    private float _lastShakeTime = 0f;

    private bool _isBubbling = false;

    private float baseBubbleIncrement = 0.2f;
    
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private ThirdPersonController _thirdPersonController;
    private bool _startedShaking;

    private void Start()
    {
        _input = _isPlayerOne ? InputManager.Instance.StarterAssetsInputsPlayerOne : InputManager.Instance.StarterAssetsInputsPlayerTwo;
        _controller = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        gameUI.UpdateBubbleCounter(0);
    }

    private void Update()
    {
        if (_input.jetpackShake)
        {
            _input.jetpackShake = false;

            ShakeJetpack();
        }

        if (_isBubbling && Time.time - _lastShakeTime > maxTimeHoldingBubblesSeconds)
        {
            _currentBubbles -= maxTimeHoldingBubblesSeconds;
            
            if (_currentBubbles < 0f) { _currentBubbles = 0; }

            gameUI.UpdateBubbleCounter(_currentBubbles);
        }

        if (_input.jetpackRelease)
        {
            _input.jetpackRelease = false; //TODO: Right now only player one jumps, we need to split the input in 2

            ReleaseBubbles();
        }

        _isBubbling = _currentBubbles > 0f;

    }

    private void ShakeJetpack()
    {
        float timeSinceLastPress = Time.time - _lastShakeTime;

        if (_currentBubbles > 1f) { return;}

        if (timeSinceLastPress >= animationLengthSeconds)
        {
            _currentBubbles += baseBubbleIncrement;

            gameUI.UpdateBubbleCounter(_currentBubbles);

            _isBubbling = true;
            _lastShakeTime = Time.time;

            FMODManager.Instance.PlaySound("event:/SFX_Shake");
        }

    }

    // Champagne pop?
    private void ReleaseBubbles()
    {
        float bubbleForce = Mathf.Sqrt(_currentBubbles) * releaseForceMultiplier;

        _thirdPersonController.SetVerticalVelocity(bubbleForce);

        _currentBubbles = 0f;
        _isBubbling = false;

        FMODManager.Instance.PlaySound("event:/SFX_Pop");
        _startedShaking = false;

        gameUI.UpdateBubbleCounter(_currentBubbles);
    }
}