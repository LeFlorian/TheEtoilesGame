using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[EventCommandInfo("Object", "Flash")]
public class FlashEvent : EventCommand
{

    public GameObject target;
    public float _speed = 1f;
    public float _time = 1f;
    private SpriteRenderer sprite ;

    private float t;

    void Awake(){
        sprite = target.GetComponent<SpriteRenderer>();
    }

	public override void Execute()
	{
		StartCoroutine(_Execute());
	}

    public IEnumerator _Execute()
    {
        t = 0;
        while(t < _time && target.activeSelf){
            //target.
            //target.SetActive (false);
            sprite.color = new Color(0, 0, 0, 0.75f);
            yield return new WaitForSeconds(_speed);
            //target.SetActive (true);
            sprite.color = new Color(255,255, 255, 1f);
            yield return new WaitForSeconds(_speed);       
            t += Time.deltaTime;
        }    
    }
}