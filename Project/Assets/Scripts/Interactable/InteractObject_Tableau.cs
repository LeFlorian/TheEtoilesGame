using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractObject_Tableau : InteractObject
{
    [SerializeField]
    private string sceneName;

    public override void Action()
    {
        SceneSwitcher.instance.ChangeScene(sceneName);
    }
}
