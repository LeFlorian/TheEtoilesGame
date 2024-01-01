using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ProjectileHandler : MonoBehaviour
{
    // Corridor
    [Serializable] 
    public class Path
    {
        public Transform startPoint;
        public Transform endPoint;

        public Vector3 getStartPoint(){ return startPoint.position;}
        public Vector3 getEndPoint(){ return endPoint.position;}
    }

    // Path pour chaque Corridor
    [SerializeField] private Path pathCorridor0; // 
    [SerializeField] private Path pathCorridor1; // 
    [SerializeField] private Path pathCorridor2; // 
    [SerializeField] private Path pathCorridor3; // 
    [SerializeField] private Path pathCorridor4; // 

    private Path[] paths; // tous les corridors

    private float speed; // Note speed

    // Collission 
    [SerializeField] private GameObject target;
    // [SerializeField] private CollisionEvent collisionEvent;

    // Init les corridors
	private void Awake()
	{
		paths = new Path[5] { pathCorridor0, pathCorridor1, pathCorridor2, pathCorridor3, pathCorridor4 };
	}

    
    private IEnumerator ShootProjectile(GameObject projectile,ProjectilData data, Vector3 from, Vector3 to){
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
		projectile.transform.LookAt(to);
		projectile.transform.position = from;
		while (Vector3.Distance(projectile.transform.position, to) > 0.2f)
		{
			Vector3 normalized = (to - projectile.transform.position).normalized;
            float num = ((data.GetMovementX() != 0f) ? data.GetMovementX() : 0f);
			float num2 = ((data.GetMovementY() != 0f) ? data.GetMovementY() : 0f);
			Vector3 vector = new Vector3(normalized.x, normalized.y, normalized.z);
			projectileRigidbody.MovePosition(projectile.transform.position + vector * speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		projectile.SetActive(value: false);
    }

}

