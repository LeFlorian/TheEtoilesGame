using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;

    public Image fader;

    private void Awake()
    {
        instance = this;
        

    }

    private void Start()
    {
        StartCoroutine(starting());
    }

    public void ChangeScene(string scene)
    {
        StartCoroutine(ChangeSceneC(scene));

    }

    IEnumerator starting()
    {
        fader.gameObject.SetActive(true);
        fader.color = Color.black;
        while (fader.color.a > 0)
        {
            fader.color -= Color.black * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fader.gameObject.SetActive(false);
    }

    IEnumerator ChangeSceneC(string scene)
    {
        fader.gameObject.SetActive(true);
        fader.color = Color.clear;
        while (fader.color.a < 1)
        {
            fader.color += Color.black*Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(scene);

    }
}
