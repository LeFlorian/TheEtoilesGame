using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;

[EventCommandInfo("Battle Events", "Shoot sinus projectile")]
[Serializable]
public class ShootSinusObstacle : EventCommand
{

    [SerializeField] public SinProjectil data;

    public override void Execute()
    {

    }
}
