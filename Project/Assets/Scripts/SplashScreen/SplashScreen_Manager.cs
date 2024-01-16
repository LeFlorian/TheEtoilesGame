using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen_Manager : MonoBehaviour
{
    public enum State
    {
        Title,
        Twitch,
        ToPlay
    }

    public State state;

    public GameObject twitchmenu;

    //public GameObject normal_game;

    //public GameObject hardcore_game;


    private void Update()
    {
        if (InputManager.instance.anyPerformed)
        {
            Debug.Log("Click");

            switch (state)
            {
                case State.Title:
                    ShowTwitchMenu();
                    Debug.Log("title");

                    break;
                    
                case State.Twitch:
                    break;
                    
                case State.ToPlay:
                    break;
            }
        }
    }

    private void ShowTwitchMenu()
    {
        twitchmenu.SetActive(true);
    }

    public void Hardcore(){
        twitchmenu.SetActive(false);

    }

}
