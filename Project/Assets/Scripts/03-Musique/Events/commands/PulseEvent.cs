using UnityEngine;
using UnityEditor;


[EventCommandInfo("Object", "Pulse")]
public class PulseEvent : EventCommand
{

    public Transform target;
    public float _size = 1.5f;
    public float _speed = 1f;
    private Vector3 _start;


    void Awake(){
        _start = target.localScale;
    }

    void Update(){
        target.localScale = Vector3.Lerp(target.localScale, _start, Time.deltaTime * _speed);
    }

	public override void Execute()
	{
		target.localScale = _start * _size;
	}

}