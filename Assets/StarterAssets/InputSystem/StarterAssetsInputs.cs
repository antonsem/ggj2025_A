using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool _aim;
		public bool _shoot;
        public bool jetpackShake;
        public bool jetpackRelease;
		public bool gameIsPaused = false;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(!sprint);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		
        // Bubble Pack
        public void OnJetpackShake(InputValue value)
        {
            JetpackShakeInput(value.isPressed);
        }

        public void OnJetpackRelease(InputValue value)
        {
            JetpackReleaseInput(value.isPressed);
        }

		public void OnPause(InputValue value)
		{
			PauseGame(value.isPressed);
		}

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		// Bubble Pack
        public void JetpackShakeInput(bool newState)
        {
            jetpackShake = newState;
        }

        public void JetpackReleaseInput(bool newState)
        {
			if(GameResources.Instance == null)
			{
				FMODManager.Instance.PlaySound("event:/MX_MainTheme");
				GameModeManager.Instance.SetMultiplayer();
				SceneManager.LoadSceneAsync(1);
			}
			else
			{
				jetpackRelease = newState;
			}
        }

		public void PauseGame(bool newState)
		{
			gameIsPaused = true;
		}


        public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			_aim = newAimState;
		}

		public void ShootInput(bool newShootState)
		{
			_shoot = newShootState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if(hasFocus && GameResources.Instance != null)
			{
				SetCursorState(cursorLocked);
			}
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}