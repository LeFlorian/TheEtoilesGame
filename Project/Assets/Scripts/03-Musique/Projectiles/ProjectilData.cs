using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public abstract class ProjectilData :  MonoBehaviour
{
	public enum CollideType
	{
		Damager,
		Event
	}

	public enum EventType
	{
		Invoke
	}

	[Space(10f)]
	public GameObject projectilPrefab;

	public int projectilID;

	[SerializeField] private GameObject target;

	[Space(10f)] public bool Jumpable;

	[SerializeField]
	[Range(0f, 100f)]
	public float speed = 20;

	[Space(10f)]
	public CollideType collideType;

	public int damage = 1;

	public EventType eventType;

	// public UnityEvent onCollide;
	[SerializeField] private CollisionEvent collisionEvent;

	public abstract void Collide();

	public abstract float GetMovement();

	public abstract float GetMovementY();

	public abstract float GetMovementX();

}
