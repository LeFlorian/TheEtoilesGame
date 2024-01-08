using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_Pomme : InteractObject
{
    public override void Action()
    {
        FindAnyObjectByType<Amour_GameManager>().CheckValidity();
    }
}
