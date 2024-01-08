using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[EventCommandInfo("Object", "Move")]
public class MoveEvent : EventCommand
{

    public GameObject target;
    // public Vector3[] points;
    // public float[] _times;


    void Awake(){
        // sprite = target.GetComponent<SpriteRenderer>();
    }

	public override void Execute()
	{
		// StartCoroutine(_Execute());
	}

    // public IEnumerator _Execute()
    // {
    //     // t = 0;
    //     // while(t < _time && target.activeSelf){
    //     //     //target.
    //     //     //target.SetActive (false);
    //     //     sprite.color = new Color(0, 0, 0, 0.75f);
    //     //     yield return new WaitForSeconds(_speed);
    //     //     //target.SetActive (true);
    //     //     sprite.color = new Color(255,255, 255, 1f);
    //     //     yield return new WaitForSeconds(_speed);       
    //     //     t += Time.deltaTime;
    //     // }    
    // }
}