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

    private void Awake()
    {
        _bubbleProjectile = GetComponent<Rigidbody>();
		_bubblesPool = GetComponentInParent<BubblesPool>();
		_originalScale = transform.localScale;
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

	private void OnTriggerEnter(Collider other)
	{
		if(((1 << other.gameObject.layer) & LayerMask.NameToLayer("Projectile")) != 0)
		{
			_bubblesPool.PopBubble(transform);
		}
	}
}
