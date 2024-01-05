using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_Collectable : InteractObject
{
    public override void Action()
    {
        Amour_GameManager gm = FindAnyObjectByType<Amour_GameManager>();

        gm.ObtainCollectable();
        gameObject.SetActive(false);
    }
}
