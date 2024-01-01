using System;


[Serializable]
public struct Note
{
	public int noteID;

	public float noteTick;

	public int noteCorridorID;

	public float noteDurationTick;

	public string noteTypeID;

	public int noteColorID;

	public Note(int noteID, float noteTick, int noteCorridorID, float noteDurationTick, string noteTypeID, int noteColorID)
	{
		this = default(Note);
		this.noteID = noteID;
		this.noteTick = noteTick;
		this.noteCorridorID = noteCorridorID;
		this.noteDurationTick = noteDurationTick;
		this.noteTypeID = noteTypeID;
		this.noteColorID = noteColorID;
	}
}
