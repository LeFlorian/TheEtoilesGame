using UnityEngine;
using UnityEngine.Events;
using System;

public class ProjectilData
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

	// public int projectilID;

	[SerializeField] public GameObject target;

	[Space(10f)] public bool Jumpable = true;

	[SerializeField]
	[Range(0f, 100f)]
	public float speed = 20;

	[Space(10f)]
	public CollideType collideType;

	public int damage = 1;

	public EventType eventType;

	// public UnityEvent onCollide;
	[SerializeField] public CollisionEvent collisionEvent;

	public virtual void Collide() { throw new NotImplementedException(); }

	public virtual float GetMovement() { return speed; }

	public virtual float GetMovementX() { return 0f; }

	public virtual float GetMovementY(){ return 0f; }

}
