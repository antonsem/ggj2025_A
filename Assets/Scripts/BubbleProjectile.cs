using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class BubbleProjectile : MonoBehaviour
{
	[SerializeField] private float _persistenceTimeMs = 10000;
	[SerializeField] private float _speed = 1;
	[SerializeField] private float _minScale = 0.1f;
	
    private Rigidbody _bubbleProjectile;
	private float _elapsedTime;
	private BubblesPool _bubblesPool;
	private Vector3 _originalScale;
	private int _projectileLayer;
	private int _friendLayer;
	private PlayerType _playerType;

    private void Awake()
    {
        _bubbleProjectile = GetComponent<Rigidbody>();
		_bubblesPool = GetComponentInParent<BubblesPool>();
		_originalScale = transform.localScale;
		_projectileLayer = LayerMask.NameToLayer("Projectile");
		_friendLayer = LayerMask.NameToLayer("Friend");
	}

	private void OnEnable()
	{
		_bubbleProjectile.linearVelocity = transform.forward * _speed;
		transform.localScale = _originalScale * Random.Range(_minScale, 1f);
		_elapsedTime = 0;
	}

	private void Update()
	{
		_elapsedTime += Time.deltaTime * 1000f;

		if(_elapsedTime > _persistenceTimeMs)
		{
			_bubblesPool.PopBubble(transform);
		}
	}

	public void SetPlayer(PlayerType playerType)
	{
		_playerType = playerType;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer != _projectileLayer)
		{
			_bubblesPool.PopBubble(transform);
		}

		if(other.gameObject.layer == _friendLayer)
		{
			MeshRenderer friendBubble = other.gameObject.GetComponent<MeshRenderer>();
			
			if(friendBubble.enabled)
			{
				friendBubble.enabled = false; //TODO: Add particle effect
				friendBubble.gameObject.GetComponent<SphereCollider>().radius = 0.25f;
			}
			else
			{
				GameData.Instance.UpdateScore(_playerType == PlayerType.PlayerOne ? PlayerType.PlayerTwo : PlayerType.PlayerOne, -1);
			}

			GameData.Instance.UpdateScore(_playerType, 1);
		}
	}
}
