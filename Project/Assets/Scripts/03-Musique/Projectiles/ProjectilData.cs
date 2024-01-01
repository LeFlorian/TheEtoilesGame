using UnityEngine;
using UnityEngine.Events;

public abstract class ProjectilData : ScriptableObject
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

	public int projectilID;

	[Space(10f)]
	public bool Jumpable;

	[Space(10f)]
	public CollideType collideType;

	public int damage = 1;

	public EventType eventType;

	public UnityEvent onCollide;

	[Space(10f)]
	public GameObject projectilPrefab;

	public abstract void Collide();

	public abstract float GetMovement();

	public abstract float GetMovementY();

	public abstract float GetMovementX();
}
