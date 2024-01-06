using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class Music_LifeController : LifeController
{

    public override void KillPlayer()
    {
        SceneSwitcher.instance.ChangeScene("Lobby");
    }
}