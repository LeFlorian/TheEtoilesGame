using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class InteractObject_Enemy : InteractObject
{
    [Header("HITING")]
    [SerializeField]
    private float force;
    [SerializeField]
    private float decreaseForce;

    public override void Action()
    {
        GetComponent<EnemyController_Clone>().HitingPlayer(transform.position-FindObjectOfType<PlayerController>().transform.position,force,decreaseForce);
    }
}
