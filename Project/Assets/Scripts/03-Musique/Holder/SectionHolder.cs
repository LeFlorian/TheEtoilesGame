using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionHolder : MonoBehaviour
{
    public string sectionID;
    public string _name;
    
	public Data.Section _lastSection { get; private set; }
    private EventCommandsGroupExecutor _eventCommandsGroupExecutor;
	
	private void Awake()
	{
		_name = base.gameObject.name;
		_eventCommandsGroupExecutor = GetComponent<EventCommandsGroupExecutor>();
	}

	public bool OnSection(Data.Section section)
	{
		if (section.sectionID != sectionID)
		{
			base.gameObject.name = _name;
			return false;
		}
		base.gameObject.name = "-> " + _name;
		_lastSection = section;
		_eventCommandsGroupExecutor.Execute();
		return true;
	}

    private void Reset()
    {
        if (GetComponent<EventsCreator>() == null)
            gameObject.AddComponent<EventsCreator>();
    }
}