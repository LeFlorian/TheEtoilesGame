using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
