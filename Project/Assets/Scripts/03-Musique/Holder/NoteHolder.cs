using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHolder : MonoBehaviour
{
    public int noteID;
    public float offset;
    public Vector2Int minMaxCorridorID = new Vector2Int(0, 4);
    private EventCommandsGroupExecutor _eventCommandsGroupExecutor;
    public string _name;

	public Data.Note _lastNote { get; private set; }

	private void Awake()
	{
		_name = base.gameObject.name;
		_eventCommandsGroupExecutor = GetComponent<EventCommandsGroupExecutor>();
	}

	public bool OnNote(Data.Note note)
	{
		// Debug.Log((note.noteID != noteID) + "  : "+ note.noteID +" "+noteID);
		if (note.noteID != noteID)
		{
			base.gameObject.name = _name;
			return false;
		}
		if (note.noteCorridorID < minMaxCorridorID.x || note.noteCorridorID > minMaxCorridorID.y)
		{
			base.gameObject.name = _name;
			return false;
		}
		base.gameObject.name = "-> " + _name;
		_lastNote = note;
		_eventCommandsGroupExecutor.Execute();
		return true;
	}

    private void Reset()
    {
        if (GetComponent<EventsCreator>() == null)
            gameObject.AddComponent<EventsCreator>();
    }
}