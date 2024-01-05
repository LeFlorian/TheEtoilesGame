using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectilData data;
    public Data.Note noteInfo;

    public ProjectileHandler corridor;

    public void Projecile(ProjectilData data,Data.Note noteInfo,ProjectileHandler corridor )
    {
        this.data = data;
        this.noteInfo = noteInfo;
        this.corridor = corridor;
    }

    // Shoot projectile
    private IEnumerator MoveProjectil( ProjectileHandler corridor ){
        Vector3 from = corridor.paths[noteInfo.noteCorridorID].getStartPoint();
        Vector3 to = corridor.paths[noteInfo.noteCorridorID].getEndPoint();
        Rigidbody projectileRigidbody = transform.GetComponent<Rigidbody>();
		transform.LookAt(to);
		transform.position = from;
		while (Vector3.Distance(transform.position, to) > 0.2f)
		{
			Vector3 normalized = (to - transform.position).normalized;
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



}
