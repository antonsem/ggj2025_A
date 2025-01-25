using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubblesPool : MonoBehaviour
{
    [SerializeField] private Transform _bubbleProjectile;
    
    //private readonly HashSet<Transform> _activeBubbles = new();
    private readonly HashSet<Transform> _inactiveBubbles = new();
    
    public Transform GetBubble(Vector3 position, Quaternion rotation)
    {
        Transform bubble = _inactiveBubbles.Count > 0 ? _inactiveBubbles.First() : Instantiate(_bubbleProjectile, transform);
        _inactiveBubbles.Remove(bubble);
        //_activeBubbles.Add(bubble);
        bubble.SetPositionAndRotation(position, rotation);
        bubble.gameObject.SetActive(false);
        return bubble;
    }

    public void PopBubble(Transform bubble)
    {
        //_activeBubbles.Remove(bubble);
        _inactiveBubbles.Add(bubble);
        bubble.gameObject.SetActive(false);
    }
}
