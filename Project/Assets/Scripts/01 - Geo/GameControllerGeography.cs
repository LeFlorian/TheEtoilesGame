using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerGeography : MonoBehaviour
{
    public int levelMax = 1;
    public int currentLevel = 0;

    private float timeBetweenPlatformFalls = 1f;

    public List<GeographyPlatform> listPlatforms;
    public List<MyScreen> listScreens;

    public GeographyPlatform spawnPlatform;

    public List<Texture2D> listQuestionsLevel;
    public List<int> listResponseIdPlatforms;

    private SpriteRenderer myRenderer;
    private Texture2D currentTexture;


    public Animator star;

    public bool canRespawn;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();

        foreach (MyScreen s in listScreens)
        {
            s.GetComponent<SpriteRenderer>().color = Color.clear;
        }

        myRenderer.color = Color.clear;

        foreach (var platform in listPlatforms)
        {
            platform.SetPlatformActive(true);
        }

        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(5f);
        yield return ChangeLevel();
    }

    public IEnumerator ChangeLevel()
    {
        canRespawn = true;

        myRenderer.color = Color.white;
        currentTexture = listQuestionsLevel[currentLevel];

        // Draw Indice
        if (myRenderer != null && currentTexture != null)
        {
            Vector3 currentScale = transform.localScale;
            Sprite nouveauSprite = Sprite.Create(currentTexture, new Rect(0, 0, currentTexture.width, currentTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            myRenderer.sprite = nouveauSprite;
            transform.localScale = currentScale;
        }

        foreach (MyScreen s in listScreens)
        {
            s.GetComponent<SpriteRenderer>().color = Color.clear;
        }

        yield return new WaitForSeconds(1.5f);

        // Draw response
        foreach (MyScreen s in listScreens)
        {
            s.GetComponent<SpriteRenderer>().color = Color.white;
            s.ChangeTexture(currentLevel);
        }

        yield return new WaitForSeconds(3f);

        canRespawn = false;
        spawnPlatform.SetPlatformActive(false);


        yield return new WaitForSeconds(3f);

        // Fall platforms
        StartCoroutine(FallPlatformsRandom());

        yield return new WaitForSeconds(listPlatforms.Count);

        // Reset 

        //TeleportPlayerToCenter();


        currentLevel++;

        if (currentLevel >= levelMax)
        {
            canRespawn = true;

            Win();
            
        }
        else
        {

            yield return new WaitForSeconds(1f);

            spawnPlatform.SetPlatformActive(true);

            foreach (var platform in listPlatforms)
            {
                platform.SetPlatformActive(true);
            }


            StartCoroutine(ChangeLevel());
        }
    }

    private IEnumerator FallPlatformsRandom()
    {
        List<int> availableIds = new List<int>();

        for (int i = 0; i < listPlatforms.Count; i++)
        {
            availableIds.Add(i);
        }

        availableIds.Remove(listResponseIdPlatforms[currentLevel]);

        while (availableIds.Count > 0)
        {
            int randomIndex = Random.Range(0, availableIds.Count);
            int randomId = availableIds[randomIndex];
            listPlatforms[randomId].SetPlatformActive(false);
            availableIds.RemoveAt(randomIndex);
            yield return new WaitForSeconds(timeBetweenPlatformFalls);
        }
    }

    private void Win()
    {
        star.SetTrigger("Win");

        spawnPlatform.SetPlatformActive(true);

        foreach (MyScreen s in listScreens)
        {
            s.GetComponent<SpriteRenderer>().color = Color.clear;
        }

        myRenderer.color = Color.clear;
    }
}
