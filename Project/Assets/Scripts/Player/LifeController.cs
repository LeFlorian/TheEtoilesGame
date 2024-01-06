using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int maxLife;
    public int life;

    public virtual int InflictDamage(int damage)
    {
        life -= damage;
        life = Mathf.Clamp(life, 0, maxLife);

        if (life <= 0)
        {
            KillPlayer();
        }

        return life;
    }

    public virtual void KillPlayer()
    {
        Debug.Log("I die");
    }
}
