using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class hVersus_GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;

    public int[] nbOfEnemyByWaves;

    private int currentWave;

    [SerializeField]
    public GameObject[] ennemyPrefab;

    [SerializeField]
    private GameObject enemyBoss;
    private bool doOnce;
    private bool doOnce2;

    [SerializeField]
    private GameObject[] plateforms;

    [SerializeField]
    private Transform playerSpawnPoint;

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject level;

    private bool canSpawnCode;

    private bool spawnedCode;

    [SerializeField]
    private float timerBoss = 63f;

    private int multiplier_code = 2;

    public int timer_multiplier = 10;


    private void Start()
    {
        RespawnPlayer();
        currentWave = -1;
        canSpawnCode = true;
        spawnedCode = false;
        StartCoroutine(BossSpawn(timerBoss));
        StartCoroutine(Multiplier(timer_multiplier));
        
        
    }

    private void Update()
    {
        if(currentWave == -1){
            StartCoroutine(Spawn());

        }
        else{
            if (canSpawnCode)
            {
                StartCoroutine(CodesSpawn(11f, UnityEngine.Random.Range(0, 2)));
                
                if (FindObjectsOfType<EnemyController_Clone>().Length < multiplier_code) spawnedCode = false;
                else spawnedCode = true;

            }
            else
            {
                
                if (!doOnce)
                {
                    foreach(GameObject go in plateforms)
                    {
                        go.GetComponent<Animator>().SetTrigger("Fall");
                    }

                    doOnce = true;

                    canvas.GetComponent<Animator>().SetTrigger("ActiveBossLevel");

                    Instantiate(enemyBoss, spawnPoints[1]);


                }
                
                if (doOnce && FindObjectsOfType<EnemyController_Clone>().Length <= 0 && !doOnce2)
                {
                    doOnce2 = true;
                        level.GetComponent<Animator>().SetTrigger("ActiveFinalScene");
                }
            }
        
        }
    }

    public void RespawnPlayer()
    {
        int actualLife = 4;
        if (player != null)
        {
            actualLife = player.GetComponent<hVersus_LifeController>().life;

            Destroy(player.GetComponent<PlayerController>()._functionnal.playerCamera);
            Destroy(player);
        }

        player = Instantiate(playerPrefab);
        player.transform.position = playerSpawnPoint.position;
        player.GetComponent<hVersus_LifeController>().life = actualLife;
        HearthController hps = GameObject.Find("LifeBar").GetComponent<HearthController>(); //GameObject.GetComponent<HearthController>();
        hps.hps = player.GetComponent<hVersus_LifeController>();
    }
    public IEnumerator Spawn(){
        if(currentWave == -1){
            yield return new WaitForSeconds(10f);
            if(currentWave == -1 ) 
            {
                currentWave = 0;
                Transform chooseSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                //GameObject ennemy = ennemyPrefab[UnityEngine.Random.Range(0, ennemyPrefab.Length)];
                GameObject ennemy = ennemyPrefab[2];

                Instantiate(ennemy, chooseSpawnPoint);
            }
        }
    }
    
    public IEnumerator CodesSpawn(float timer, int multiplier){
        yield return new WaitForSeconds(timer);
        if(!spawnedCode){
            for (int i = 0; i < multiplier; i++)
            {
                Transform chooseSpawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                GameObject ennemy = ennemyPrefab[UnityEngine.Random.Range(0, ennemyPrefab.Length)];
                Instantiate(ennemy, chooseSpawnPoint);
            }
            spawnedCode = true;
        }
        
    }

    public IEnumerator BossSpawn(float timer){
        yield return new WaitForSeconds(timer);
        canSpawnCode = false;
    }

    public IEnumerator Multiplier(float timer){
        while(canSpawnCode){
            yield return new WaitForSeconds(timer);
            multiplier_code = (int)Math.Round(multiplier_code*1.5);
            Debug.Log( multiplier_code);
        }
    }
}
