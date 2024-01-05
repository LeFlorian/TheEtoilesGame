using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;

[EventCommandInfo("Battle Events", "Shoot projectile")]
[Serializable] 
public class ShootObstacle : EventCommand
{
    [SerializeField] public CommonProjectil data;

    public override void Execute()
    {

    }
}