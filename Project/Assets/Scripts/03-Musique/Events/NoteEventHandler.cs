
using UnityEngine;


namespace EventHolder {
public class NoteEventHandler : MonoBehaviour
{
	public int noteID;
	public float offset;
	public Vector2Int minMaxCorridorID = new Vector2Int(0, 4);
	public ChartReader customChartReader;
	private string _name;
	private ChartReader _chartReader;
	private EventCommandsGroupExecutor _eventCommandsGroupExecutor;
	private int _currentNoteIndex;
	private bool _active = true;

	public Data.Note _lastNote { get; private set; }

	// private void Reset()
	// {
	// 	if (GetComponent<EventCommandsGroupExecutor>() == null)
	// 	{
	// 		base.gameObject.AddComponent<EventCommandsGroupExecutor>();
	// 	}
	// }

	// private void Awake()
	// {
	// 	_name = base.gameObject.name;
	// 	_eventCommandsGroupExecutor = GetComponent<EventCommandsGroupExecutor>();
	// 	_chartReader = ((customChartReader == null) ? Object.FindObjectOfType<Everhood.Chart.ChartReader>() : customChartReader);
	// }

	// private void Update()
	// {
	// 	if (!_chartReader.Active || !_active || _currentNoteIndex >= _chartReader._notes.Count || !(_chartReader._songposition >= _chartReader._notes[_currentNoteIndex].noteTick * _chartReader._ticks + offset))
	// 	{
	// 		return;
	// 	}
	// 	int num = 1;
	// 	OnNote(_chartReader._notes[_currentNoteIndex]);
	// 	for (int i = 1; i < 5; i++)
	// 	{
	// 		if (_currentNoteIndex + i < _chartReader._notes.Count && _chartReader._notes[_currentNoteIndex].noteTick == _chartReader._notes[_currentNoteIndex + i].noteTick)
	// 		{
	// 			OnNote(_chartReader._notes[_currentNoteIndex + i]);
	// 			num++;
	// 		}
	// 	}
	// 	_currentNoteIndex += num;
	// }

	// public void OnNote(Data.Note note)
	// {
	// 	if (note.noteID != noteID)
	// 	{
	// 		base.gameObject.name = _name;
	// 		return;
	// 	}
	// 	if (note.noteCorridorID < minMaxCorridorID.x || note.noteCorridorID > minMaxCorridorID.y)
	// 	{
	// 		base.gameObject.name = _name;
	// 		return;
	// 	}
	// 	base.gameObject.name = "-> " + _name;
	// 	_lastNote = note;
	// 	_eventCommandsGroupExecutor.Execute();
	// }

	// public void IgnoreNotesUntilNextNote()
	// {
	// 	_active = false;
	// 	_currentNoteIndex = 0;
	// 	for (int i = 0; i < _chartReader._notes.Count; i++)
	// 	{
	// 		if (_currentNoteIndex < _chartReader._notes.Count)
	// 		{
	// 			if (!(_chartReader._songposition >= _chartReader._notes[_currentNoteIndex].noteTick * _chartReader._ticks + offset))
	// 			{
	// 				break;
	// 			}
	// 			_currentNoteIndex++;
	// 		}
	// 	}
	// 	_active = true;
	// }

	// public static void Pass()
	// {
	// 	NoteEventHandler[] array = Object.FindObjectsOfType<NoteEventHandler>();
	// 	for (int i = 0; i < array.Length; i++)
	// 	{
	// 		array[i].IgnoreNotesUntilNextNote();
	// 	}
	// }
}
}