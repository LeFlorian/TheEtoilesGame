using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHolder : MonoBehaviour
{
    public int noteID;
    public float offset;
    public Vector2Int minMaxCorridorID = new Vector2Int(0, 4);
    private EventCommandsGroupExecutor _eventCommandsGroupExecutor;
    private string _name;

	public Data.Note _lastNote { get; private set; }

	private void Awake()
	{
		_name = base.gameObject.name;
		_eventCommandsGroupExecutor = GetComponent<EventCommandsGroupExecutor>();
	}

	public void OnNote(Data.Note note)
	{
		if (note.noteID != noteID)
		{
			base.gameObject.name = _name;
			return;
		}
		if (note.noteCorridorID < minMaxCorridorID.x || note.noteCorridorID > minMaxCorridorID.y)
		{
			base.gameObject.name = _name;
			return;
		}
		base.gameObject.name = "-> " + _name;
		_lastNote = note;
		_eventCommandsGroupExecutor.Execute();
	}

    private void Reset()
    {
        if (GetComponent<EventsCreator>() == null)
            gameObject.AddComponent<EventsCreator>();
    }
}