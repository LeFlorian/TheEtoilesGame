using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LobbyManager : MonoBehaviour
{
    public Transform spawnEndCinematic;

    private void Start()
    {
        /*PlayerPrefs.SetInt($"AsCompletedLvl{1}",1);
        PlayerPrefs.SetInt($"AsCompletedLvl{2}",1);
        PlayerPrefs.SetInt($"AsCompletedLvl{3}",1);
        PlayerPrefs.SetInt($"AsCompletedLvl{4}",1);
        PlayerPrefs.SetInt($"AsCompletedLvl{5}",1);*/

        CheckAsFinishTheGame();
    }

    private void CheckAsFinishTheGame()
    {
        bool[] levelValid = new bool[5];

        for (int i = 1; i < 6; i++)
            Verify(i);

        void Verify(int level)
        {
            levelValid[level-1] = PlayerPrefs.HasKey($"AsCompletedLvl{level}");
        }

        bool allValid = true;

        for (int i = 0;i < 5; i++)
        {
            if (!levelValid[i])
            {
                allValid = false;
                break;
            }
        }

        if (allValid)
        {
            Win();
        }
       
    }

    private void Win()
    {
        FindAnyObjectByType<PlayerController>().transform.position = spawnEndCinematic.position;

        PlayerPrefs.DeleteKey($"AsCompletedLvl{1}");

        PlayerPrefs.DeleteKey($"AsCompletedLvl{2}");

        PlayerPrefs.DeleteKey($"AsCompletedLvl{3}");

        PlayerPrefs.DeleteKey($"AsCompletedLvl{4}");

        PlayerPrefs.DeleteKey($"AsCompletedLvl{5}");
    }

    IEnumerator waitToGoCredits()
    {
        yield return new WaitForSeconds(5);
        SceneSwitcher.instance.ChangeScene("Credits");
    }
}
