using UnityEngine;
using System;
using UnityEngine.Events;


public class EventsCreator : MonoBehaviour{ }

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class EventCommandInfo : Attribute
{
    /// <summary>
    /// Metadata atribute for the Command class. 
    /// </summary>
    /// <param name="category">The category to place this command in.</param>
    /// <param name="commandName">The display name of the command.</param>
        public EventCommandInfo(string category, string commandName)
    {
        this.Category = category;
        this.CommandName = commandName;
    }

    public string Category { get; set; }
    public string CommandName { get; set; }
    
}


// public class NoteEvents
// {
// 	[Serializable]
// 	public class OnNote : UnityEvent<Data.Note>{}
// 	public OnNote onNote;
// 	public OnNote onNoteRight;
// 	public OnNote onNoteLeft;
// }

// public class SectionEvents 
// {
// 	[Serializable]
// 	public class OnSection : UnityEvent<Data.Section>{}
// 	public string sectionName;
// 	[Space(5f)]
// 	public OnSection onSection;
// 	public SectionEvents(string sectionName)
// 	{
// 		this.sectionName = sectionName;
// 	}
// }


// public class CustomEvents
// {
// 	public string sectionName;
// 	public int eventPosition;
// 	[Space(5f)]
// 	public UnityEvent onCustom;
// }
