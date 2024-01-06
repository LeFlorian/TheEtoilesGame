using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class CollisionEvent : MonoBehaviour
{
	public static readonly Dictionary<Tuple<GameObject, GameObject>, CollisionEvent> targetedCollisionEventCache = new Dictionary<Tuple<GameObject, GameObject>, CollisionEvent>();

	public static void Execute(GameObject collider, GameObject targetCollision)
	{
		if (targetedCollisionEventCache.TryGetValue(new Tuple<GameObject, GameObject>(collider, targetCollision), out var value))
		{
			value.Execute();
		}
	}
	// public void AddToCache(GameObject collider, GameObject targetCollision)
	// {
	// 	targetedCollisionEventCache.Add(new Tuple<GameObject, GameObject>(collider, targetCollision), this);
	// }

	public abstract void Execute();
}
