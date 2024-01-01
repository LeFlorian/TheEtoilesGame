using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonProjectil", menuName = "Audiowarrior/Projectils/CommonProjectil", order = 1)]
public class CommonProjectil : ProjectilData
{
	[SerializeField]
	[Range(0f, 100f)]
	private float speed;

	public override void Collide()
	{
		throw new NotImplementedException();
	}

	public override float GetMovement()
	{
		return speed;
	}

	public override float GetMovementX()
	{
		return 0f;
	}

	public override float GetMovementY()
	{
		return 0f;
	}
}
