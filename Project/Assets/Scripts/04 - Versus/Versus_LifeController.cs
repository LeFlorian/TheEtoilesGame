using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Versus_LifeController : LifeController
{

    public override int InflictDamage(int damage)
    {
        base.InflictDamage(damage);

        FindObjectOfType<Versus_GameManager>().RespawnPlayer();

        return life;
    }

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}
