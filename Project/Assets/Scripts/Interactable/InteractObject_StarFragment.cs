using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_StarFragment : InteractObject
{
    public int levelID;

    public override void Action()
    {
        PlayerPrefs.SetInt($"AsCompletedLvl{levelID}", 1);

        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}
