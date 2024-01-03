using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// using UnityEngine.AddressableAssets;

// Chart : contient Notes et Sections
public class Chart : MonoBehaviour
{
    // Rangement des notes créées
	public const string notesParentName = "NOTES";
	public const string sectionsParentName = "SECTIONS";
    [SerializeField] private DefaultAsset chart;
    public AudioSource song;
    public Data.ChartData chartData;

    [ContextMenu("Generate Chart Data")]
    public void GenerateData()
    {
        chartData = ChartGenerator.UpdateChart(chart);
        CreateNotesLayout();
        CreateSectionsLayout();
    }

    // Créer tous les objets notes
    private void CreateNotesLayout()
	{
        Parents.NotesParent notesParent = GetComponentInChildren<Parents.NotesParent>();
        List<int> notesIDs = GetNotesIDs();
        // Si les notes n'existent pas encore
        if (notesParent == null)  createNewNotes(notesIDs);
        else UpdateNotes(notesParent, notesIDs); // verifie les notes déjà créées
    }

    private void UpdateNotes(Parents.NotesParent notesParent,List<int> notesIDs){
        Slots.NoteSlot[] noteSlotComponents = notesParent.GetComponentsInChildren<Slots.NoteSlot>(); // Recup les notes
        foreach (Slots.NoteSlot noteSlot in noteSlotComponents){
            bool noteInUse = false;
            int usedId = 0;
            foreach (int noteID in notesIDs){
                if (noteSlot.noteID == noteID) {
                    noteInUse = true;
                    usedId = noteID;
                    break;
                }
            }
            string noteName = $"Note - ID = {usedId}";
            if (!noteInUse) noteName = noteName + " [Not In Use]";
            noteSlot.gameObject.name = noteName;
            
        }
        foreach (int noteID in notesIDs)
        {
            bool noteExist = false;
            foreach (Slots.NoteSlot noteSlot in noteSlotComponents)
            {
                if (noteID == noteSlot.noteID) {noteExist = true; break;}
            }
            if (!noteExist) createNewNote(GameObject.Find(notesParentName), noteID);
        } 
    }

    private void createNewNotes(List<int> notesIDs){
        GameObject noteParent = new GameObject(notesParentName);
        noteParent.transform.SetParent(this.transform);
        noteParent.AddComponent<Parents.NotesParent>();
        foreach (int noteID in notesIDs) createNewNote(noteParent, noteID);
    }

    private void createNewNote(GameObject noteParent ,int noteID){
        string noteName = $"Note - ID = {noteID}";
        Transform noteSlot = CreateNoteSlot(noteParent, noteName, noteID);
        CreateNoteEvenHandler(noteSlot.gameObject, "Event", noteID); // ?? a voir si c'est utile
    }
    
    public Slots.NoteHolder CreateNoteEvenHandler(GameObject noteParent, string noteName, int noteID)
    {
        GameObject noteEvent = new GameObject(noteName);
        noteEvent.transform.SetParent(noteParent.transform);
        Slots.NoteHolder noteEventHandler = noteEvent.AddComponent<Slots.NoteHolder>();
        noteEventHandler.noteID = noteID;

        return noteEventHandler;
    }


    private List<int> GetNotesIDs()
    {
        List<int> id = new List<int>();
        foreach (Data.Note note in chartData.notes)
        {
            int ID = note.noteID;
            if (!id.Contains(ID)) id.Add(ID);
        }
        return id;
    }

	public Transform CreateNoteSlot(GameObject noteParent, string noteName, int noteID)
	{
		GameObject obj = new GameObject(noteName);
		obj.transform.SetParent(noteParent.transform);
		obj.AddComponent<Slots.NoteSlot>().noteID = noteID;
		return obj.transform;
	}

    // Créer tous les objets sections
    public void CreateSectionsLayout()
    {
        Parents.SectionsParent sectionsParent = GetComponentInChildren<Parents.SectionsParent>();
        List<Data.Section> theSections = chartData.sections;
        if (sectionsParent == null) createNewSections(theSections);
        else UpdateSections(sectionsParent, theSections);
    }
    
    private void UpdateSections(Parents.SectionsParent sectionsParent, List<Data.Section> theSections){
        Slots.SectionHolder[] sectionEventHandlers = sectionsParent.GetComponentsInChildren<Slots.SectionHolder>();

        foreach (Slots.SectionHolder sectionSlot in sectionEventHandlers)
        {
            bool sectionUsed = false;
            string usedID = "";
            foreach (Data.Section section in theSections)
            {
                if (sectionSlot.sectionID == section.sectionID) {
                    sectionUsed = true;
                    usedID = section.sectionID;
                    break;
                }
            }
            string sectionName = $"Section - {usedID}";
            if (!sectionUsed) sectionName = sectionName + " [Not In Use]";
            sectionSlot.gameObject.name = sectionName;
            
        }
        foreach (Data.Section section in theSections)
        {
            bool sectionExist = false;
            foreach (Slots.SectionHolder sectionSlot in sectionEventHandlers)
            {
                if (section.sectionID == sectionSlot.sectionID) {sectionExist = true; break;}
            }
            if (!sectionExist) createNewSection(GameObject.Find(sectionsParentName) , section.sectionID);
        } 
    }
    private void createNewSections(List<Data.Section> theSections ){
        GameObject sectionParent = new GameObject(sectionsParentName);
        sectionParent.transform.SetParent(this.transform);
        sectionParent.AddComponent<Parents.SectionsParent>();
        foreach (Data.Section section in theSections) createNewSection(sectionParent, section.sectionID);
    }

    private void createNewSection(GameObject sectionParent ,string sectionID){
        string sectionName = $"Section - {sectionID}";
        GameObject sectionSlot = new GameObject(sectionName);
        sectionSlot.transform.SetParent(sectionParent.transform);

        Slots.SectionHolder sectionEventHandler = sectionSlot.AddComponent<Slots.SectionHolder>();
        sectionEventHandler.sectionID = sectionID;
    }
    


}
