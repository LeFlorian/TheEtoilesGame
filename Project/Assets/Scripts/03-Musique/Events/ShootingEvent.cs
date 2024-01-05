using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Events {
[Serializable]
public class ShootProjectile : EventCommand
{
    [SerializeField] public ProjectileHandler corridor;
	[SerializeField] private ProjectilData data;
	[SerializeField] private EventHolder.NoteEventHandler noteEventHandler;
	[Space(10f)]
	[SerializeField] private int projectilePoolSize = 20;
	public override void Execute()
	{
		if (base.enabled)
		{
			int corridorId = GetCorridor();
			GameObject projectile = Pool.PoolManager.instance.ReuseObject(data.projectilPrefab, corridor.paths[corridorId].getStartPoint(), Quaternion.identity);
			Vector3 projectileStartPosition = corridor.paths[corridorId].getStartPoint();
			Vector3 to = corridor.paths[corridorId].getEndPoint();
			StartCoroutine(MoveProjectil(projectile, projectileStartPosition, to));
		}
	}

	private void Reset()
	{
		noteEventHandler = GetComponent<EventHolder.NoteEventHandler>();
	}

	public int GetCorridor()
	{
		if (noteEventHandler == null)
		{
			noteEventHandler = GetComponent<EventHolder.NoteEventHandler>();
		}
		return noteEventHandler._lastNote.noteCorridorID;
	}

    // Shoot projectile
    private IEnumerator MoveProjectil(GameObject projectile,Vector3 from, Vector3 to ){
                
        Rigidbody projectileRigidbody = projectile.transform.GetComponent<Rigidbody>();
		transform.LookAt(to);
		transform.position = from;
		while (Vector3.Distance(projectile.transform.position, to) > 0.2f)
		{
			Vector3 normalized = (to - projectile.transform.position).normalized;
            float num = data.GetMovementX();
			float num2 = data.GetMovementY();
			Vector3 vector = new Vector3(normalized.x, normalized.y, normalized.z);
			projectileRigidbody.MovePosition(transform.position + vector * data.speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		transform.gameObject.SetActive(value: false);
    }

    private bool NotReachedTarget(float zDirection, bool positive)
	{
		if (positive) return zDirection > 0f;
		return zDirection < 0f;
	}

	public void ResolveRefs(ProjectilData data){
		this.data = data;
	}

	public void ResolveRefs(ProjectileHandler corridor, int poolSize, EventHolder.NoteEventHandler noteEventHandler){
		this.corridor = corridor;
		this.noteEventHandler = noteEventHandler;
		this.projectilePoolSize = poolSize;
	}	



	}
}