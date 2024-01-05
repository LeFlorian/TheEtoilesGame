
using UnityEngine;


namespace EventHolder {
public class SectionEventHandler : MonoBehaviour
{
	public string sectionID;

	[SerializeField] private bool dontExecuteIfImageSensitive;

	private string _name;

	// private EventCommandsGroupExecutor _eventCommandsGroupExecutor;

	private bool _active = true;

	// public EventCommandsGroupExecutor EventCommandsGroupExecutor => _eventCommandsGroupExecutor;

	// public void SetSectionHandlerState(bool state)
	// {
	// 	if (!state)
	// 	{
	// 		base.gameObject.name = "[Deactivated] " + _name;
	// 	}
	// 	else
	// 	{
	// 		base.gameObject.name = _name;
	// 	}
	// 	_active = state;
	// }

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
	// 	if (EverhoodGameData.GetInstance().settingsData.imageSensitive && dontExecuteIfImageSensitive)
	// 	{
	// 		base.gameObject.SetActive(value: false);
	// 	}
	// }

	// public void OnSection(Data.Section section)
	// {
	// 	if (!base.gameObject.activeInHierarchy || !base.gameObject.activeSelf || !_active)
	// 	{
	// 		return;
	// 	}
	// 	if (section.sectionID != sectionID)
	// 	{
	// 		base.gameObject.name = _name;
	// 		return;
	// 	}
	// 	base.gameObject.name = "-> " + _name;
	// 	if (_eventCommandsGroupExecutor != null)
	// 	{
	// 		_eventCommandsGroupExecutor.Execute();
	// 	}
	// }
}

}