using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geo_LifeController : LifeController
{
    public Transform point;

    public override int InflictDamage(int damage)
    {
        int life = base.InflictDamage(damage);

        StartCoroutine(WaitingRespawn());

        return life;
    }

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }

    private IEnumerator WaitingRespawn()
    {
        while (FindAnyObjectByType<GameControllerGeography>().canRespawn == false)
        {
            yield return new WaitForSeconds(1);
        }

        transform.position = point.position;

    }
}
