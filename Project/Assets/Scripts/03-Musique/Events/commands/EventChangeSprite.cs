using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

[EventCommandInfo("Section", "Change Sprite")]
public class EventChangeSprite : EventCommand
{

	[Space(5f)]
    [SerializeField] public GameObject target;
	[Space(5f)]
	// [SerializeField] public ProjectilData data;
	
    public Sprite newSprite;
    public RuntimeAnimatorController newAnimatorController;

	public override void Execute()
	{
		target.GetComponent<SpriteRenderer>().sprite = newSprite;
        target.GetComponent<Animator>().runtimeAnimatorController = newAnimatorController;
	}

}