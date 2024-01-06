using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<LifeController>().InflictDamage(1);
        }

        if (collision.tag == "Interactable")
        {
            Destroy(collision.gameObject);
        }
    }
}
