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

    [SerializeField]
    private bool isBoss;

    public override void Action()
    {
        if (isBoss)
        {
            TwitchCharger tc = FindAnyObjectByType<TwitchCharger>();

            if (tc != null)
            {
                if (tc._addingAMessage.value >= tc._addingAMessage.maxValue)
                {
                    force = 10;
                }
            }

            GetComponent<EnemyController_Clone>().HitingPlayer(transform.position - FindObjectOfType<PlayerController>().transform.position, force, decreaseForce);
        }
        else
        {
            GetComponent<EnemyController_Clone>().HitingPlayer(transform.position - FindObjectOfType<PlayerController>().transform.position, force, decreaseForce);
        }
    }
}
