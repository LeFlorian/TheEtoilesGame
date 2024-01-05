using System;
using UnityEngine;

[Serializable]
public class SinProjectil : ProjectilData
{
    [SerializeField] public CommonProjectil data;
	
	[SerializeField] [Range(0f, 100f)] private float waveHeightX = 0;

	[SerializeField] [Range(0f, 100f)] private float waveSpeedX = 0;

	[SerializeField] [Range(0f, 100f)] private float waveHeightY = 0;

	[SerializeField] [Range(0f, 100f)] private float waveSpeedY = 0;

    [Space(5)]
    public bool customStartTimeSinWave = false;
    public float startTimeSinWaveX = 0;
    public float startTimeSinWaveY = 0;

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
