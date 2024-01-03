using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SinProjectil", menuName = "Audiowarrior/Projectils/SinProjectil")]
public class SinProjectil : ProjectilData
{
	[SerializeField] [Range(0f, 100f)] private float speed;

	[SerializeField] [Range(0f, 100f)] private float waveHeightX;

	[SerializeField] [Range(0f, 100f)] private float waveSpeedX;

	[SerializeField] [Range(0f, 100f)] private float waveHeightY;

	[SerializeField] [Range(0f, 100f)] private float waveSpeedY;

	public float WaveHeightX => waveHeightX;

	public float WaveSpeedX => waveSpeedX;

	public float WaveHeightY => waveHeightY;

	public float WaveSpeedY => waveSpeedY;

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
		return waveHeightX * Mathf.Sin(Time.time * waveSpeedX);
	}

	public override float GetMovementY()
	{
		return waveHeightY * Mathf.Sin(Time.time * waveSpeedY);
	}
}
