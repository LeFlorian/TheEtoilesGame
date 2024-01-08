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
        if (collision.gameObject.tag == "Player")
        {
			Music_LifeController hp = collision.gameObject.GetComponent<Music_LifeController>();
			hp.InflictDamage(damage);
			// Debug.Log("Hit Player "+damage+" "+hp.life+"Hp left");
			Destroy(gameObject);
        }
    }

}