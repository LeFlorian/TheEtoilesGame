// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Everhood.DamagePlayerCollisionEvent
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerCollisionEvent : CollisionEvent
{
	[SerializeField] private GameObject player;
	[SerializeField] private int damage = 1;
	[SerializeField] private bool jumpable;
	[SerializeField] private bool overrideDeflectVisualAsJumpable;
	[SerializeField] private bool overrideDeflectVisualAsNotJumpable;

	public bool IsJumpable => jumpable;
	public bool OverrideDeflectVisualAsJumpable => overrideDeflectVisualAsJumpable;
	public bool OverrideDeflectVisualAsNotJumpable => overrideDeflectVisualAsNotJumpable;

	public void ResolveRefs(GameObject battlePlayer, bool jumpable)
	{
		player = battlePlayer;
		this.jumpable = jumpable;
	}

	private void Reset()
	{
		// player = Object.FindObjectOfType<BattlePlayer>();
	}

	public override void Execute()
	{
		// if (!player.jumping || !jumpable)
		// 	player.Damage(damage);
	}
}