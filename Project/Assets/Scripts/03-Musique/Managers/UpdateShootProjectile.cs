using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpdateShootProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileHandler corridors;
    [SerializeField] private GameObject target;

    [ContextMenu("update Projectiles")]
    public void updateProjectiles(){
        if (target == null) corridors = UnityEngine.Object.FindObjectOfType<ProjectileHandler>();
        if (target == null) target = GameObject.Find("Player");
        EventCommand[] obstacles = UnityEngine.Object.FindObjectsOfType<ShootObstacle>();
        updateProjectile(target, obstacles);
        EventCommand[] sinobstacles = UnityEngine.Object.FindObjectsOfType<ShootSinusObstacle>();
        updateProjectile(target, sinobstacles);
    }


    public void updateProjectile(GameObject target, EventCommand[] obstacles){

        foreach (ShootObstacle obstacle in obstacles)
        {
            GameObject gameObject = UnityEngine.Object.Instantiate(obstacle.data.projectilPrefab); 
            gameObject.gameObject.SetActive(value: false);
            // gameObject2.AddComponent<DontGoThroughThings>();
            Events.ShootProjectile shootProjectileCommand = obstacle.gameObject.AddComponent<Events.ShootProjectile>();
            DamagePlayerCollisionEvent damagePlayerCollisionEvent = obstacle.gameObject.AddComponent<DamagePlayerCollisionEvent>();
            damagePlayerCollisionEvent.ResolveRefs(target, obstacle.data.Jumpable);
            shootProjectileCommand.ResolveRefs(obstacle.data);
            shootProjectileCommand.ResolveRefs( corridors, 100, obstacle.gameObject.GetComponent<EventHolder.NoteEventHandler>());
            // shootProjectileEventCommand.INIT();
        }



        }
}


