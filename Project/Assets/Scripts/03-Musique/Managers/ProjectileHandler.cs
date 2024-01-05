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

    public Path[] paths; // tous les corridors

    private float speed; // Note speed

    // Collission 
    [SerializeField] private GameObject target;
    // [SerializeField] private CollisionEvent collisionEvent;

    // Init les corridors
	private void Awake()
	{
		paths = new Path[5] { pathCorridor0, pathCorridor1, pathCorridor2, pathCorridor3, pathCorridor4 };
	}

    


}

