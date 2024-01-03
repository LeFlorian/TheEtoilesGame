// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class NotesReader : MonoBehaviour
// {


// 	[Serializable]
// 	public class OnEvent
// 	{
// 		[Space(10f)]
// 		public OnBeats[] onBeats;

// 		[SerializeField]
// 		[Space(10f)]
// 		private OnSection[] onSection;

// 		[Space(10f)]
// 		public SongState songState;

// 		[Space(10f)]
// 		public UnityEvent OnCheckpoint;

// 		[HideInInspector]
// 		public float[] NoteDuration = new float[5];

// 		public void OnNote(Note noteData, int phaseIndex)
// 		{
// 			phaseIndex--;
// 			if (phaseIndex < 0)
// 			{
// 				phaseIndex = 0;
// 			}
// 			onBeats[noteData.NoteNum].onBeat.corridorsAttacks.Invoke(noteData.CorridorPos, noteData.NoteNum);
// 		}

// 		public void PhaseChange(int index)
// 		{
// 			if (index < onSection.Length)
// 			{
// 				onSection[index].onPhase.Invoke();
// 			}
// 			MonoBehaviour.print(" ----> " + onSection[index].Phase);
// 		}
// 	}

// 	[Serializable]
// 	public class OnBeats
// 	{
// 		public string name;

// 		[Space(10f)]
// 		public OnBeat onBeat;

// 		[Space(10f)]
// 		public OnBeatAnticipation onBeatAnticipation;
// 	}

// 	[Serializable]
// 	public class OnBeat
// 	{
// 		[Space(10f)]
// 		[Header("On Beat")]
// 		[Space(10f)]
// 		public UnityEvent EventOnBeat;

// 		[Space(10f)]
// 		public UnityEvent OnBeatRight;

// 		[Space(10f)]
// 		public UnityEvent OnBeatLeft;

// 		[Space(10f)]
// 		public CorridorsAttack corridorsAttacks;
// 	}

// 	[Serializable]
// 	public class OnBeatAnticipation
// 	{
// 		public float AnticipationSeconds;

// 		[Space(10f)]
// 		[Header("On Beat")]
// 		[Space(10f)]
// 		public UnityEvent EventOnBeat;

// 		[Space(10f)]
// 		public UnityEvent OnBeatRight;

// 		[Space(10f)]
// 		public UnityEvent OnBeatLeft;
// 	}

// 	[Serializable]
// 	public class OnSection
// 	{
// 		public string Phase;

// 		[Space(10f)]
// 		[Header("Notes")]
// 		[Space(10f)]
// 		public UnityEvent onPhase;
// 	}

// 	[Serializable]
// 	public class SongState
// 	{
// 		[Space(10f)]
// 		[Header("On song Start")]
// 		[Space(10f)]
// 		public UnityEvent onStart;

// 		[Space(10f)]
// 		[Header("On song End")]
// 		[Space(10f)]
// 		public UnityEvent onEnd;
// 	}

// 	[Serializable]
// 	public class CorridorsAttack : UnityEvent<int, int>
// 	{
// 	}

// 	[Space(5f)]
// 	[Header("DEBUG")]
// 	[Space(5f)]
// 	[Range(0f, 300f)]
// 	public float SecondToJump;

// 	public float SecondToDecay;

// 	[Space(5f)]
// 	[Header("General")]
// 	[Space(5f)]
// 	[SerializeField]
// 	private TextAsset NotesChart;

// 	[SerializeField]
// 	private TextAsset PhaseChart;

// 	public AudioSource song;

// 	[Space(5f)]
// 	[Header("Events")]
// 	[Space(5f)]
// 	public OnEvent onEvent;

// 	[Space(5f)]
// 	[Header("Checkpoint")]
// 	[Space(5f)]
// 	public float secondsAnticipation;

// 	public List<Checkpoint> _checkpointData = new List<Checkpoint>();

// 	public static int CheckpointIndex;

// 	[Space(5f)]
// 	[Header("Settings")]
// 	[Space(5f)]
// 	public float Bpm;

// 	[SerializeField]
// 	private int Resolution;

// 	[SerializeField]
// 	private float offset;

// 	private string[] data;

// 	private string[] dataPhase;

// 	private List<Note> _NoteData = new List<Note>();

// 	private List<Phases> _phasesData = new List<Phases>();

// 	private int CurrentIndex;

// 	private int CurrentAnticipationIndex;

// 	private int CurrentPhaseIndex;

// 	private int CurrentCheckpointIndex;

// 	private float dsptimesong;

// 	public bool Pause;

// 	public static NotesReader instance;

// 	public float tick { get; private set; }

// 	public float FirstTick { get; private set; }

// 	public float songposition { get; private set; }

// 	public bool StartSong { get; private set; }

// 	private void Awake()
// 	{
// 		instance = this;
// 		CurrentIndex = 0;
// 		tick = 60000f / (Bpm * (float)Resolution);
// 		tick /= 1000f;
// 		MonoBehaviour.print(tick);
// 		data = NotesChart.text.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None);
// 		dataPhase = PhaseChart.text.Split(new string[2] { "\r\n", "\n" }, StringSplitOptions.None);
// 		for (int i = 0; i < data.Length; i++)
// 		{
// 			string[] array = data[i].Split(' ');
// 			int num = int.Parse(array[0]);
// 			int corridorPos = int.Parse(array[3]);
// 			int num2 = int.Parse(array[4]);
// 			int noteColor = int.Parse(array[3]);
// 			int noteNum = int.Parse(array[5]);
// 			string noteType = array[2];
// 			_NoteData.Add(Note.createNote(num, num2, corridorPos, noteColor, noteType, noteNum));
// 		}
// 		for (int j = 0; j < dataPhase.Length; j++)
// 		{
// 			string[] array2 = dataPhase[j].Split(' ');
// 			int num3 = int.Parse(array2[0]);
// 			string phaseName = array2[3];
// 			_phasesData.Add(Phases.createPhase(num3, phaseName));
// 		}
// 	}

// 	private void SkipToEnd()
// 	{
// 		CurrentAnticipationIndex = 0;
// 		CurrentIndex = 0;
// 		CurrentPhaseIndex = 0;
// 		StartSong = false;
// 		dsptimesong = (float)AudioSettings.dspTime - SecondToJump;
// 		song.time = SecondToJump;
// 		StartCoroutine(Skip());
// 	}

// 	public void StartsSong(float Delay)
// 	{
// 		if (CheckpointIndex == 0)
// 		{
// 			StartCoroutine(StartBattle(Delay));
// 			return;
// 		}
// 		onEvent.songState.onStart.Invoke();
// 		dsptimesong = (float)AudioSettings.dspTime;
// 		song.Play();
// 		StartSong = true;
// 		SecondToJump = _checkpointData[CheckpointIndex - 1].NotesPosicion;
// 		SkipToEnd();
// 	}

// 	private IEnumerator StartBattle(float Delay)
// 	{
// 		yield return new WaitForSeconds(Delay);
// 		onEvent.songState.onStart.Invoke();
// 		dsptimesong = (float)AudioSettings.dspTime;
// 		song.Play();
// 		StartSong = true;
// 		SkipToEnd();
// 	}

// 	public void NewBpm(float bpm)
// 	{
// 		Bpm = bpm;
// 		tick = 60000f / (Bpm * (float)Resolution);
// 		tick /= 1000f;
// 		MonoBehaviour.print("BPM : " + Bpm + " TICK : " + tick);
// 	}

// 	public void FirstNewBpm(float bpm)
// 	{
// 		Bpm = bpm;
// 		tick = 60000f / (Bpm * (float)Resolution);
// 		tick /= 1000f;
// 		FirstTick = tick;
// 		MonoBehaviour.print("BPM : " + Bpm + " TICK : " + tick);
// 	}

// 	public void JumpPosChange(float value)
// 	{
// 		SecondToJump = value;
// 		SkipToEnd();
// 	}

// 	public void SetSongState(bool state)
// 	{
// 		Pause = state;
// 	}

// 	private void FixedUpdate()
// 	{
// 		ReadSong();
// 	}

// 	private void ReadSong()
// 	{
// 		if (!StartSong || Pause)
// 		{
// 			return;
// 		}
// 		float num = (float)(AudioSettings.dspTime - (double)dsptimesong);
// 		songposition = num * song.pitch - offset;
// 		if (CurrentIndex < _NoteData.Count && songposition >= _NoteData[CurrentIndex].NotesPosicion * tick)
// 		{
// 			int num2 = 1;
// 			if (_NoteData[CurrentIndex].NoteType != "S")
// 			{
// 				onEvent.onBeats[_NoteData[CurrentIndex].NoteNum].onBeat.EventOnBeat.Invoke();
// 				onEvent.OnNote(_NoteData[CurrentIndex], CurrentPhaseIndex);
// 				for (int i = 1; i < 5; i++)
// 				{
// 					if (CurrentIndex + i < _NoteData.Count && _NoteData[CurrentIndex].NotesPosicion == _NoteData[CurrentIndex + i].NotesPosicion)
// 					{
// 						num2++;
// 						onEvent.OnNote(_NoteData[CurrentIndex + i], CurrentPhaseIndex);
// 					}
// 				}
// 				if (_NoteData[CurrentIndex].CorridorPos <= 2)
// 				{
// 					onEvent.onBeats[_NoteData[CurrentIndex].NoteNum].onBeat.OnBeatRight.Invoke();
// 				}
// 				else
// 				{
// 					onEvent.onBeats[_NoteData[CurrentIndex].NoteNum].onBeat.OnBeatLeft.Invoke();
// 				}
// 			}
// 			CurrentIndex += num2;
// 			if (CurrentIndex >= _NoteData.Count)
// 			{
// 				MonoBehaviour.print(" ----> END");
// 				onEvent.songState.onEnd.Invoke();
// 			}
// 		}
// 		if (CurrentAnticipationIndex < _NoteData.Count && songposition >= _NoteData[CurrentAnticipationIndex].NotesPosicion * tick - onEvent.onBeats[_NoteData[CurrentAnticipationIndex].NoteNum].onBeatAnticipation.AnticipationSeconds)
// 		{
// 			int num3 = 1;
// 			if (_NoteData[CurrentAnticipationIndex].NoteType != "S")
// 			{
// 				onEvent.onBeats[_NoteData[CurrentAnticipationIndex].NoteNum].onBeatAnticipation.EventOnBeat.Invoke();
// 				for (int j = 1; j < 5; j++)
// 				{
// 					if (CurrentAnticipationIndex + j < _NoteData.Count && _NoteData[CurrentAnticipationIndex].NotesPosicion == _NoteData[CurrentAnticipationIndex + j].NotesPosicion)
// 					{
// 						num3++;
// 					}
// 				}
// 				if (_NoteData[CurrentAnticipationIndex].CorridorPos <= 2)
// 				{
// 					onEvent.onBeats[_NoteData[CurrentAnticipationIndex].NoteNum].onBeatAnticipation.OnBeatRight.Invoke();
// 				}
// 				else
// 				{
// 					onEvent.onBeats[_NoteData[CurrentAnticipationIndex].NoteNum].onBeatAnticipation.OnBeatLeft.Invoke();
// 				}
// 			}
// 			CurrentAnticipationIndex += num3;
// 		}
// 		if (CurrentPhaseIndex < _phasesData.Count && songposition >= _phasesData[CurrentPhaseIndex].NotesPosicion * tick)
// 		{
// 			onEvent.PhaseChange(CurrentPhaseIndex);
// 			CurrentPhaseIndex++;
// 		}
// 		if (CurrentCheckpointIndex < _checkpointData.Count && songposition >= _checkpointData[CurrentCheckpointIndex].NotesPosicion - secondsAnticipation)
// 		{
// 			onEvent.OnCheckpoint.Invoke();
// 			CurrentCheckpointIndex++;
// 		}
// 	}

// 	private IEnumerator Skip()
// 	{
// 		float num = (float)(AudioSettings.dspTime - (double)dsptimesong);
// 		songposition = num * song.pitch - offset;
// 		for (int i = 0; i < _NoteData.Count; i++)
// 		{
// 			if (CurrentIndex < _NoteData.Count)
// 			{
// 				if (!(songposition >= _NoteData[CurrentIndex].NotesPosicion * tick))
// 				{
// 					break;
// 				}
// 				int num2 = 1;
// 				CurrentIndex += num2;
// 				CurrentAnticipationIndex += num2;
// 			}
// 		}
// 		for (int j = 0; j < _checkpointData.Count; j++)
// 		{
// 			if (CurrentCheckpointIndex < _checkpointData.Count)
// 			{
// 				if (!(songposition >= _checkpointData[CurrentCheckpointIndex].NotesPosicion - secondsAnticipation))
// 				{
// 					break;
// 				}
// 				int num3 = 1;
// 				CurrentCheckpointIndex += num3;
// 			}
// 		}
// 		StartSong = true;
// 		yield return null;
// 	}
// }
