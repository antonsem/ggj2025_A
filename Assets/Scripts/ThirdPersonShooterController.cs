using StarterAssets;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
	[SerializeField] private CinemachineCamera _aimCamera;
	[SerializeField] private float _normalSensitivity = 1f;
	[SerializeField] private float _aimSensitivity = 0.5f;
	[SerializeField] private LayerMask _aimColliderLayerMask = new();
	[SerializeField] private Transform _pointer;
	[SerializeField] private Transform _bubbleProjectile;
	[SerializeField] private Transform _spawnBubblePosition;
	

	private StarterAssetsInputs _starterAssetsInputs;
	private ThirdPersonController _thirdPersonController;

	private void Awake()
	{
		_starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		_thirdPersonController = GetComponent<ThirdPersonController>();
	}

	private void Update()
	{
		Vector3 mouseWorldPosition = Vector3.zero;
		
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		//ray.direction = Camera.main.transform.forward;
		if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
		{
			//_pointer.position = Mouse.current.position.value;
			_pointer.position = raycastHit.point;
			mouseWorldPosition = raycastHit.point;
		}

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

		if(_starterAssetsInputs._shoot)
		{
			Vector3 aimDirection = (mouseWorldPosition - _spawnBubblePosition.position).normalized;
			Instantiate(_bubbleProjectile, _spawnBubblePosition, Quaternion.LookRotation(aimDirection, Vector3.up));
			_starterAssetsInputs._shoot = false;
		}
	}
}
