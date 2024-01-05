using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class UpdateShootProjectile
{
    public ProjectileHandler corridors;
    public GameObject target;

    public void updateProjectiles(ProjectileHandler corridors){
        if (corridors == null) this.corridors = UnityEngine.Object.FindObjectOfType<ProjectileHandler>();
        if (target == null) target = GameObject.Find("Player");
        ShootObstacle[] obstacles = UnityEngine.Object.FindObjectsOfType<ShootObstacle>();
        updateProjectile(obstacles);
        ShootSinusObstacle[] sinobstacles = UnityEngine.Object.FindObjectsOfType<ShootSinusObstacle>();
        updateProjectile(sinobstacles);
    }

    public void updateProjectile( ShootProjectile[] obstacles){
        foreach (ShootProjectile obstacle in obstacles)
        {
            obstacle.initTarget(target);
            obstacle.corridors = corridors;
            obstacle.noteEventHandler = obstacle.gameObject.GetComponent<NoteHolder>();
            // // gameObject2.AddComponent<DontGoThroughThings>();
            // DamagePlayerCollisionEvent damagePlayerCollisionEvent = obstacle.gameObject.AddComponent<DamagePlayerCollisionEvent>();
            // damagePlayerCollisionEvent.ResolveRefs(target, obstacle.data.Jumpable);
            obstacle.INIT();
        }
    }
}



