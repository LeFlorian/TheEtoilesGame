using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVersus : MonoBehaviour
{
    public float damageAmount;
    public float maxDamage;

    public float AddingDamage()
    {
        damageAmount += Random.Range(0.05f, 0.1f);
        damageAmount = Mathf.Clamp01(damageAmount);

        return damageAmount;
    }
}
