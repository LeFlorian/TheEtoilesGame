using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;

[EventCommandInfo("Battle Events", "Shoot projectile")] 
public class ShootObstacle : ShootProjectile
{
    [SerializeField] public CommonProjectil data;

  public  override ProjectilData getData(){
		return data;
	}
  public override void initTarget(GameObject target){
    data.target = target;
  }
}