using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo_LifeController : LifeController
{
    public Transform point;

    public override int InflictDamage(int damage)
    {
        int life = base.InflictDamage(damage);

        transform.position = point.position;

        return life;
    }

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}
