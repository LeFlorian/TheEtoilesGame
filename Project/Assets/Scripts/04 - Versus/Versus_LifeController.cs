using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Versus_LifeController : LifeController
{
    [SerializeField]
    private Transform respawnPoint;

    public override int InflictDamage(int damage)
    {
        base.InflictDamage(damage);

        transform.position = respawnPoint.position;
        return life;
    }

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}
