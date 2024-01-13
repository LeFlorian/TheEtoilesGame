using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher instance;

    public Image fader;

    public int speed = 1;

    public Vector3 spawn = new Vector3(0f,0f,0f);

    private int nbLevels = 5;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

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
        Vector3 pos = spawn;
        fader.gameObject.SetActive(true);
        fader.color = Color.clear;
        while (fader.color.a < 1)
        {
            fader.color += Color.black*Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone) yield return null;
        GameObject Player = GameObject.FindWithTag("Player");
        Player.transform.position = pos;
        yield return new WaitForEndOfFrame();
        fader.gameObject.SetActive(false);
        // starting();
        // SceneManager.LoadScene(scene);
    }

    public void Reset(){
        Debug.Log("Deleted player files!");
        for(int i = 1; i < nbLevels+1; i++){
            try{
                PlayerPrefs.DeleteKey($"AsCompletedLvl{i}");
            
            }
            catch{
                Debug.Log(i);
            }
        }
        ChangeScene("Lobby");

    }
}
