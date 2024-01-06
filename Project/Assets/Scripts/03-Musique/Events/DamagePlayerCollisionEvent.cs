// Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Everhood.DamagePlayerCollisionEvent
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DamagePlayerCollisionEvent : MonoBehaviour
{
	[SerializeField] public GameObject player;
	[SerializeField] public int damage = 1;
	[SerializeField] public bool jumpable;
	// [SerializeField] private bool overrideDeflectVisualAsJumpable;
	// [SerializeField] private bool overrideDeflectVisualAsNotJumpable;

	public bool IsJumpable => jumpable;
	// public bool OverrideDeflectVisualAsJumpable => overrideDeflectVisualAsJumpable;
	// public bool OverrideDeflectVisualAsNotJumpable => overrideDeflectVisualAsNotJumpable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
			Debug.Log("colision");
			// player = gameObject.GetComponent;
			if (!jumpable){
				Debug.Log("Hit ! ");
				collision.gameObject.GetComponent<LifeController>().InflictDamage(damage);
			}	
        }

        if (collision.tag == "Interactable")
        {
            Destroy(collision.gameObject);
        }
    }

	// public override void Execute()
	// {
	// 	if (!player.jumping || !jumpable)
	// 		player.GetComponent<LifeController>().InflictDamage(damage);
	// }
}