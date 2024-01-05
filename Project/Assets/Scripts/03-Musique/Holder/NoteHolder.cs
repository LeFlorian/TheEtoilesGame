using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHolder : MonoBehaviour
{
    public int noteID;
    public float offset;

    public int minCorridorID = 0;
    public int maxCorridorID = 4;

    private void Reset()
    {
        if (GetComponent<EventsCreator>() == null)
            gameObject.AddComponent<EventsCreator>();
    }
}