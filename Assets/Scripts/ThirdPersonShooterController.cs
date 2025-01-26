using StarterAssets;
using System;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class ThirdPersonShooterController : MonoBehaviour
{
	[SerializeField] private CinemachineCamera _aimCamera;
	[SerializeField] private float _normalSensitivity = 1f;
	[SerializeField] private float _aimSensitivity = 0.5f;
	[SerializeField] private LayerMask _aimColliderLayerMask = new();
	[SerializeField] private Transform _pointer;
	[SerializeField] private Transform _spawnBubblePosition;
	[SerializeField] private float _projectileForwardDistance = 100f;
	[SerializeField] private int _additionalBubbles = 9;
	[SerializeField] private float _spreadAngle = 0.5f;
	[SerializeField] private float _timeBetweenProjectiles = 0.5f;
	[SerializeField] private BubblesPool _bubblesPool;
	[SerializeField] private bool _isPlayerOne;

	private StarterAssetsInputs _starterAssetsInputs;
	private ThirdPersonController _thirdPersonController;
	private float _timeSinceLastProjectile;
	private Camera _camera;

	private void Start()
	{
		_starterAssetsInputs = InputManager.Instance.StarterAssetsInputs;
		_thirdPersonController = GetComponent<ThirdPersonController>();
		_camera = GameObject.FindGameObjectWithTag(_isPlayerOne ? "MainCamera" : "MainCameraP2").GetComponent<Camera>();
	}

	private void Update()
	{
		Vector2 screenCenterPoint = new Vector2(Screen.width / 4f, Screen.height / 2f);
		if(!_isPlayerOne)
		{
			screenCenterPoint = new Vector2(screenCenterPoint.x + Screen.width / 2f, screenCenterPoint.y);
		}
		
		
		Ray ray = _camera.ScreenPointToRay(screenCenterPoint);
		//ray.direction = Camera.main.transform.forward;
		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
		{
			_pointer.position = raycastHit.point;
		}
		else
		{
			_pointer.position = ray.origin + ray.direction * 999f;
		}

		Vector3 mouseWorldPosition = _pointer.position;

		if(_starterAssetsInputs._aim)
		{
			_aimCamera.gameObject.SetActive(true);
			_thirdPersonController.Sensitivity = _aimSensitivity;
			_thirdPersonController.RotateOnMove = false;

			Vector3 worldAimTarget = mouseWorldPosition;
			worldAimTarget.y = transform.position.y;
			Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

			transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
		}
		else
		{
			_aimCamera.gameObject.SetActive(false);
			_thirdPersonController.Sensitivity = _normalSensitivity;
			_thirdPersonController.RotateOnMove = true;
		}

		_timeSinceLastProjectile += Time.deltaTime;
		if(_starterAssetsInputs._shoot && _timeSinceLastProjectile > _timeBetweenProjectiles)
		{
			Vector3 aimDirection = mouseWorldPosition == Vector3.zero ? (_camera.transform.forward * _projectileForwardDistance - _spawnBubblePosition.position).normalized  : (mouseWorldPosition - _spawnBubblePosition.position).normalized;
			Quaternion mainRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
			
			for(int i = 0; i < _additionalBubbles; i++)
			{
				Transform bubble = _bubblesPool.GetBubble(_spawnBubblePosition.position, mainRotation);
				//bubble.transform.rotation = bubble.transform.rotation * Quaternion.AngleAxis((_spreadAngle / 2f) - ((i / (_additionalBubbles / 2f)) * (_spreadAngle / 2f)), Vector3.up);
				bubble.transform.forward = PickFiringDirection(bubble.transform.forward, _spreadAngle);
				bubble.gameObject.SetActive(true);
			}

			Transform mainBubble = _bubblesPool.GetBubble(_spawnBubblePosition.position, mainRotation);
			mainBubble.gameObject.SetActive(true);
			//_starterAssetsInputs._shoot = false;
			_timeSinceLastProjectile = 0;

			FMODManager.Instance.SetGlobalParameterValue("IsShooting", 1);
			FMODManager.Instance.PlaySound("event:/SFX_Shooting");
		}
		else if(!_starterAssetsInputs._shoot)
		{
			FMODManager.Instance.SetGlobalParameterValue("IsShooting", 0);
		}
	}

	Vector3 PickFiringDirection(Vector3 muzzleForward, float spreadRadius)
	{
		Vector3 candidate = Random.insideUnitSphere * spreadRadius + muzzleForward;
		return candidate.normalized;
	}
}
