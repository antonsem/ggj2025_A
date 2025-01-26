using System;
using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        [SerializeField] private bool _isPlayerOne;
        
        private StarterAssetsInputs starterAssetsInputs;

        private void Start()
        {
            starterAssetsInputs = _isPlayerOne ? InputManager.Instance.StarterAssetsInputsPlayerOne : InputManager.Instance.StarterAssetsInputsPlayerTwo;
        }

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }
        
    }

}
