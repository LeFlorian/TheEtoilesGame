using System;


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
