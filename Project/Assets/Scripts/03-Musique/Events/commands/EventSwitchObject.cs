using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.PackageManager;

[Serializable]

[EventCommandInfo("Section", "Switch Object")]
public class EventSwitchObject : EventCommand
{

	[Space(5f)]
    [SerializeField] public GameObject From;
    [SerializeField] public GameObject To;
	// [SerializeField] public ProjectilData data;

	public override void Execute()
	{
		From.SetActive (false);
		To.SetActive (true);
	}

}