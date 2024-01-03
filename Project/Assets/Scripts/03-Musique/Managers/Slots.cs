using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots
{
    public class NoteSlot : MonoBehaviour
    {
        public int noteID;
    }

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

    public class SectionHolder : MonoBehaviour
    {
        public string sectionID;
        private void Reset()
        {
            if (GetComponent<EventsCreator>() == null)
                gameObject.AddComponent<EventsCreator>();
        }
    }

}
