using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public Transform spawnEndCinematic;

    private void Start()
    {

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
    }

    IEnumerator waitToGoCredits()
    {
        yield return new WaitForSeconds(5);
        SceneSwitcher.instance.ChangeScene("Credits");
    }
}
