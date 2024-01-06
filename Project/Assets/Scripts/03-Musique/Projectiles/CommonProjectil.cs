using System;
using UnityEngine;

[Serializable]
public class CommonProjectil : ProjectilData
{
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