using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AsyncOperations;

[Serializable]
public class ChartReader : MonoBehaviour
{
    [SerializeField] private ChartData chart;
    private AudioSource audioSource;
	// private bool forceAddresableRelease;
	private int _currentNoteIndex;
	// private int _currentSectionIndex;
	// private float _previousFrameDspTime;
	// private float _lastReportedPlayheadPosition;
	// private float _dspSongStartTime;
	private bool _active = true;
	private bool _started;
	// private float _pauseOffset;
	// private Parents.SectionsParent _inUseSectionParent;

    /// <summary>
    [SerializeField] private bool startItSelf;
	[SerializeField] private float startItSelfDelay;
	[SerializeField] private float offset;
	[SerializeField] private NoteHolder[] noteHolder;
	[Space(10f)] [SerializeField] private float delayAnticipation;
	// [Space(10f)] [SerializeField] private List<SectionEvents> sectionEvents = new List<SectionEvents>();
	// [Space(10f)] [SerializeField] private List<CustomEvents> customEvents = new List<CustomEvents>();

	// private int _currentNoteAnticipationIndex;
	// private int _currentCustomIndex;
	private float _dsptimesong;
	// private Coroutine _musicRead;
    /// </summary>

	private bool __init = false;
	public float _tick { get; private set; }
	public List<Data.Note> _notes { get; private set; }
	public List<Data.Section> _sections { get; private set; }
	public float _songposition { get; private set; }
	public bool Active => _active;
	public bool Playing => audioSource.isPlaying;
	public bool Started => _started;


	[ContextMenu("Update Chart Reader")]
    public void UpdateReader()
    {
		audioSource = chart.song;
		_tick = chart.chartData.tick;
		_notes = chart.chartData.notes;
		_sections = chart.chartData.sections;
		noteHolder = FindObjectsOfType<NoteHolder>();
		__init = true;
    }



    // Init Music
	private void Awake()
	{
		if (!__init) UpdateReader();
		// _tick = chart.chartData.tick;
		// _notes = chart.chartData.notes;
		// _sections = chart.chartData.sections;
        // audioSource = chart.song;
	}

    private void Start(){
        if (startItSelf) StartMusic(startItSelfDelay);
    }

    public void StartMusic(float delay) { StartCoroutine(DoPlay(delay)); }
    
	public IEnumerator DoPlay(float startDelay, bool FromStart = true)
	{
		yield return new WaitForSeconds(startDelay);
		if (FromStart)
		{
			audioSource.time = 0f;
			_dsptimesong = (float)AudioSettings.dspTime;
			// _currentCustomIndex = 0;
			// _currentNoteAnticipationIndex = 0;
			_currentNoteIndex = 0;
			// _currentSectionIndex = 0;
			audioSource.Play();
		}
		Debug.Log("Loop : ");
		while ( _active)
		{

			float num = (float)(AudioSettings.dspTime - (double)_dsptimesong);
			_songposition = num * audioSource.pitch - offset;
			ReadNotes();
			// ReadAnticipedNotes();
			// ReadSections();
			// ReadCustomEvents();
			yield return null;
		}
		// Change scene // cinematique
	}
    
    private void ReadNotes()
	{
		if ( (_currentNoteIndex + 1 ) < _notes.Count && _songposition >= _notes[_currentNoteIndex].noteTick * _tick)
		{
			Data.Note note = _notes[_currentNoteIndex];
			_ = _songposition;
			_ = _notes[_currentNoteIndex].noteTick;
			_ = _tick;
			int num = 1;
			if (_notes[_currentNoteIndex].noteTypeID != "S")
			{
				noteHolder[note.noteID].OnNote(note);
               
				for (int i = 1; i < 5; i++)
				{
					if ((_currentNoteIndex + 1 + i) < _notes.Count && _notes[_currentNoteIndex].noteTick == _notes[_currentNoteIndex + i].noteTick)
					{
						num++;
						note = _notes[_currentNoteIndex + i];
						noteHolder[note.noteID].OnNote(note);

					}
				}
				// if (_notes[_currentNoteIndex].noteCorridorID <= 2) noteEvents.onNoteRight.Invoke(_notes[_currentNoteIndex]);
				// else noteEvents.onNoteLeft.Invoke(_notes[_currentNoteIndex]);

			}
			_currentNoteIndex += num;
			_ = _currentNoteIndex;
			_ = _notes.Count;
		}

		if (_currentNoteIndex > _notes.Count ){
			_active = false;
		}
	}

	// private void ReadSections()
	// {
	// 	if (_currentSectionIndex < _sections.Count && _songposition >= _sections[_currentSectionIndex].sectionTick * _tick)
	// 	{
	// 		sectionEvents[_currentSectionIndex].onSection.Invoke(_sections[_currentSectionIndex]);
	// 		_currentSectionIndex++;
	// 	}
	// }

	public void Restart() { StartMusic(0f); }

}