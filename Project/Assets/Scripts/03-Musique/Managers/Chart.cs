using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Chart : MonoBehaviour
{
    // Rangement des notes créées
	public const string notesParentName = "NOTES";
	public const string sectionsParentName = "SECTIONS";

    // Les parametres de la musique
    public DefaultAsset chart; // Document contenant les notes 
    public AudioSource audioSource; // le fichier audio
    public string songName; // Le nom
    public int subdivision; // Type de note (noire, croche, double ...)
    public int bpm; // Nb noire par minutes
    public float tick; // durée d'un tick

    // Notes
	public List<Note> notes = new List<Note>();

	public List<Section> sections = new List<Section>();

    [ContextMenu("Generate Chart Data")]
    public void GenerateData()
    {
        ChartGenerator.UpdateChart(this, chart);
        // CreateNotesLayout();
        // CreateSectionsLayout(this.transform, chartData.sections);
    }



}
