using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Data
{
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

    [Serializable]
    public struct Section
    {
        public string sectionID;
        public float sectionTick;
        public Section(string sectionID, float sectionTick)
        {
            this.sectionID = sectionID;
            this.sectionTick = sectionTick;
        }
    }

	// Chart : contient Notes et Sections
    [Serializable]
	public class ChartData
	{
        // Les infos sur la musique
		public string songName; // Nom de la musique
		public int resolution; // Type de note (noire, croche, double ...)
		public int bpm; // Nb noire par minutes
		public float tick; // durée d'un tick
        // Notes
		public List<Note> notes = new List<Note>();
		public List<Section> sections = new List<Section>();
	}

}
