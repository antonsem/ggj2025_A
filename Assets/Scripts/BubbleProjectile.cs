using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BubbleProjectile : MonoBehaviour
{
    private Rigidbody _bubbleProjectile;

    private void Awake()
    {
        _bubbleProjectile = GetComponent<Rigidbody>();
    }

    private void Start()
	{
		float speed = 10f;
		_bubbleProjectile.angularVelocity = transform.forward * speed;
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}
}
