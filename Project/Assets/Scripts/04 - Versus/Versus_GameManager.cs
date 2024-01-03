using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Versus_GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public int[] nbOfEnemyByWaves;

    private int currentWave;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyBoss;
    private bool doOnce;

    [SerializeField]
    private GameObject[] plateforms;

    private void Update()
    {
        if (currentWave < nbOfEnemyByWaves.Length)
        {
            if (FindObjectsOfType<EnemyController>().Length <= 0)
            {
                for (int i = 0; i < nbOfEnemyByWaves[currentWave]; i++)
                {
                    Transform chooseSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    Instantiate(enemyPrefab, chooseSpawnPoint);
                }

                currentWave += 1;
            }
        }
        else
        {
            if (FindObjectsOfType<EnemyController>().Length <= 0)
            {
                if (!doOnce)
                {
                    foreach(GameObject go in plateforms)
                    {
                        go.GetComponent<Animator>().SetTrigger("Fall");
                    }

                    doOnce = true;

                    Instantiate(enemyBoss, spawnPoints[1]);
                }
            }
        }
    }
}
