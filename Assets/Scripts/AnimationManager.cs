using StarterAssets;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    [SerializeField] private bool _isPlayerOne;
    
    private StarterAssetsInputs _starterAssetsInputs;
    private Animator _animator;
    private bool _isShooting;
    
    private static readonly int _extractGun = Animator.StringToHash("ExtractGun");
    private static readonly int _removeGun = Animator.StringToHash("RemoveGun");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _starterAssetsInputs = _isPlayerOne ? InputManager.Instance.StarterAssetsInputsPlayerOne : InputManager.Instance.StarterAssetsInputsPlayerTwo;
    }

    private void Update()
    {
        if(_isShooting)
        {
            if(!_starterAssetsInputs._aim && !_starterAssetsInputs._shoot)
            {
                StopShooting();
            }
        }
        else
        {
            if(_starterAssetsInputs._aim || _starterAssetsInputs._shoot)
            {
                Shoot();
            }
        }
    }

    public void FootStep(float theValue)
    {
        FMODManager.Instance.PlaySound("event:/SFX_Footsteps");
    }

    public void Shoot()
    {
        _animator.SetTrigger(_extractGun);
        _isShooting = true;
    }

    public void StopShooting()
    {
        _animator.SetTrigger(_removeGun);
        _isShooting = false;
    }

    public void Move(Vector2 direction)
    {
        
    }
}
