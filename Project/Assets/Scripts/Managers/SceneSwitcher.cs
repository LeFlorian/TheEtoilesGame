using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;
    private void Awake()
    {
        instance = this;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
