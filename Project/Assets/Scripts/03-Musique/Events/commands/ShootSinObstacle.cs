using UnityEngine;
using System;

[EventCommandInfo("Battle Events", "Shoot sinus projectile")]

public class ShootSinusObstacle : ShootProjectile
{
  [SerializeField] public SinProjectil data;
    
  public  override ProjectilData getData(){
		return data;
	}
    
  public override void initTarget(GameObject target){
    data.target = target;
  }
}
