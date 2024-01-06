// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Everhood.DamagePlayerCollisionEvent
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DamagePlayerCollisionEvent : MonoBehaviour
{
	// [SerializeField] public GameObject player;
	[SerializeField] public int damage = 1;
	// [SerializeField] public bool jumpable;
	// [SerializeField] private bool overrideDeflectVisualAsJumpable;
	// [SerializeField] private bool overrideDeflectVisualAsNotJumpable;

	// public bool IsJumpable => jumpable;
	// public bool OverrideDeflectVisualAsJumpable => overrideDeflectVisualAsJumpable;
	// public bool OverrideDeflectVisualAsNotJumpable => overrideDeflectVisualAsNotJumpable;

    private void OnTriggerEnter(Collider collision)
    {
		Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
			LifeController hp = collision.gameObject.GetComponent<LifeController>();
			hp.InflictDamage(damage);
			Debug.Log("Hit Player "+damage+" "+hp.life+"Hp left");
			Destroy(gameObject);
        }
    }

	// public override void Execute()
	// {
	// 	if (!player.jumping || !jumpable)
	// 		player.GetComponent<LifeController>().InflictDamage(damage);
	// }
}