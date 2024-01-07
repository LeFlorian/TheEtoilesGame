using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_ButtonEnigme : InteractObject
{
    public override void Action()
    {
        if (FindAnyObjectByType<Enigme_GameManager>().CheckValidity())
        {
            Destroy(this.gameObject);
        }
    }
}
