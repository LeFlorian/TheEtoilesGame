using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using System;


public abstract class EventCommand : MonoBehaviour
{
    public abstract void Execute();
}
