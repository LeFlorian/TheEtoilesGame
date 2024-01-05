// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Everhood.BatchedCollisionEvent
using System.Collections.Generic;
using UnityEngine;

public class BatchedCollisionEvent : CollisionEvent
{
	[SerializeField]
	private List<CollisionEvent> collisionEvents = new List<CollisionEvent>();

	public List<CollisionEvent> CollisionEvents => collisionEvents;

	public override void Execute()
	{
		for (int i = 0; i < collisionEvents.Count; i++)
		{
			collisionEvents[i].Execute();
		}
	}
}
