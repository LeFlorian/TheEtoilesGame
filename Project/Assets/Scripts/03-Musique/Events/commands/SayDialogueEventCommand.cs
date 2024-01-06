using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;

[EventCommandInfo("Dialogue", "Say")]
public class SayDialogueEventCommand : EventCommand
{
    public enum SayTextType
    {
        Normal, Jittering
    }
    public Color dialogueColor;
    public AudioClip characterSpeakingSoundEffect;
    [Space(10)]
    public float duration;
    public float startDelay = 0;
    [TextArea] public string say;
    public SayTextType sayTextType;
    public bool executeJustOneTime = false;
    public override void Execute(){}
}
