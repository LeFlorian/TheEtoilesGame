using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ShootProjectile : EventCommand
{
	[Space(5f)]
    [SerializeField] public ProjectileHandler corridors;
	[Space(5f)]
	// [SerializeField] public ProjectilData data;
	
	[Space(5f)]
	[SerializeField] public NoteHolder noteEventHandler;
	[Space(5f)]

	public static float localProjectilSpeedModifier = 1f;
	// public static readonly Dictionary<Tuple<GameObject, GameObject>, GameObject> projectilesWithTarget = new Dictionary<Tuple<GameObject, GameObject>, GameObject>();
	// public static readonly HashSet<GameObject> projectiles = new HashSet<GameObject>();
	// public static readonly HashSet<GameObject> unjumpablesProjectiles = new HashSet<GameObject>();
	private DamagePlayerCollisionEvent collisionEvent;

	public virtual ProjectilData getData(){
		return new ProjectilData();
	}

	public virtual void initTarget(GameObject target){}


	public override void Execute()
	{
		_Execute();
	}

	public virtual void  _Execute(){
		if (base.enabled)
		{	ProjectilData data = getData();
			int corridorId = GetCorridor();
			GameObject projectile = UnityEngine.Object.Instantiate(data.projectilPrefab, this.transform);
			copyTo(projectile);
			Vector3 projectileStartPosition = corridors.paths[corridorId].getStartPoint();
			Vector3 to = corridors.paths[corridorId].getEndPoint();
			StartCoroutine(MoveProjectil(projectile, projectileStartPosition, to, data));
		}
	}

	private void copyTo(GameObject projectile){
		DamagePlayerCollisionEvent coll = projectile.AddComponent<DamagePlayerCollisionEvent>();
		ProjectilData  data = getData();
		// coll.jumpable = data.IsJumpable;
		coll.damage = data.damage;
		// coll.player = data.target;
	}

	private void Reset()
	{
		noteEventHandler = GetComponent<NoteHolder>();
	}

	public int GetCorridor()
	{
		if (noteEventHandler == null)
		{
			noteEventHandler = GetComponent<NoteHolder>();
		}
		return noteEventHandler._lastNote.noteCorridorID;
	}

    // Shoot projectile
    private IEnumerator MoveProjectil(GameObject projectile,Vector3 from, Vector3 to, ProjectilData data){
        Rigidbody projectileRigidbody = projectile.transform.GetComponent<Rigidbody>();
		projectile.transform.LookAt(to);
		projectile.transform.position = from;
		Vector3 direction = (to - projectile.transform.position).normalized;
		bool positive = direction.z > 0f;
		float time = Time.time;
		while (NotReachedTarget(direction.z, positive) && projectile != null)
		{
			direction = (to - projectile.transform.position).normalized;
            float num = data.GetMovementX();
			float num2 = data.GetMovementY();
			Vector3 vector = new Vector3(direction.x + num, direction.y + num2, direction.z);
			projectileRigidbody.MovePosition( projectile.transform.position + vector * ( data.speed * localProjectilSpeedModifier) * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		Destroy(projectile);
    }

    private bool NotReachedTarget(float zDirection, bool positive)
	{
		if (positive) return zDirection > 0f;
		return zDirection < 0f;
	}


	public void INIT()
	{
		localProjectilSpeedModifier = 1f;
	}
}
