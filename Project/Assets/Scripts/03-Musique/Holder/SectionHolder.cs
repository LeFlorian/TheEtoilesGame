using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionHolder : MonoBehaviour
{
    public string sectionID;
    private void Reset()
    {
        if (GetComponent<EventsCreator>() == null)
            gameObject.AddComponent<EventsCreator>();
    }
}