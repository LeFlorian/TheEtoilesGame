using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            pc.AllowInteract(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            pc.DisallowInteract(this);
        }
    }

    public virtual void Action()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
