using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_Enemy : InteractObject
{
    public override void Action()
    {
        GetComponent<EnemyController>().TakeDamage();
    }
}
