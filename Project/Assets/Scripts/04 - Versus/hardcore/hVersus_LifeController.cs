using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class hVersus_LifeController : LifeController
{

    private void Start(){
        base.InflictDamage(3);
    }
    public override int InflictDamage(int damage)
    {
        base.InflictDamage(damage);

        FindObjectOfType<hVersus_GameManager>().RespawnPlayer();

        return life;
    }

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby Hardcore");
    }
}
