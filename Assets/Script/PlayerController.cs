using System;
using FMOD.Studio;
using StarterAssets;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Bubble Settings")]
    [SerializeField] public float baseBubbleIncrement = 0.1f;
    [SerializeField] public float animationLengthSeconds = 0.3f;
    [SerializeField] private float fastPressBonusSeconds = 1f;
    [SerializeField] public float maxTimeHoldingBubblesSeconds = 2f;
    [SerializeField] public float bubbleDecayOvertime = 0.01f;
    [SerializeField] public float releaseForceMultiplier = 10f;

    // Current bubble count
    private float _currentBubbles = 0f;

    private float _lastShakeTime = 0f;
    [SerializeField][Range(1f,2f)] public float fastPressBonusMultiplier = 1.1f;

    private bool _isBubbling = false;

    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private ThirdPersonController _thirdPersonController;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (_input.jetpackShake)
        {
            _input.jetpackShake = false;

            ShakeJetpack();
        }

        // If it has been too long since last press of jetpackShake -> release bubbles
        if (_isBubbling && Time.time - _lastShakeTime > maxTimeHoldingBubblesSeconds)
        {
            _currentBubbles -= maxTimeHoldingBubblesSeconds;
            Debug.Log($"current bubbles reduced: {_currentBubbles}"); // Tsssss sound
        }

        if (_input.jetpackRelease)
        {
            _input.jetpackRelease = false;

            ReleaseBubbles();
        }

        _isBubbling = _currentBubbles > 0f;

    }

    // shaking sound
    private void ShakeJetpack() 
    {
        float timeSinceLastPress = Time.time - _lastShakeTime;

        if (timeSinceLastPress >= animationLengthSeconds && timeSinceLastPress <= (animationLengthSeconds + fastPressBonusSeconds))
        {
            _currentBubbles += baseBubbleIncrement * fastPressBonusMultiplier;
        }
        else
        {
            _currentBubbles += baseBubbleIncrement;
        }

        _isBubbling = true;
        _lastShakeTime = Time.time;

        Debug.Log($"Shaking - current bubbles: {_currentBubbles}");
    }

    // Champagne pop?
    private void ReleaseBubbles()
    {
        float bubbleForce = Mathf.Sqrt(_currentBubbles) * releaseForceMultiplier;

        _thirdPersonController.SetVerticalVelocity(bubbleForce);

        Debug.Log($"{_currentBubbles} released with bubbleForce: {bubbleForce}");

        _currentBubbles = 0f;
        _isBubbling = false;
    }
}