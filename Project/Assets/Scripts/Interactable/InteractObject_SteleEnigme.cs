using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject_SteleEnigme : InteractObject
{
    [SerializeField]
    private Sprite[] visuels;

    public int position;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        ChangeImage();
    }

    public override void Action()
    {
        position += 1;

        position = position%visuels.Length;

        FindAnyObjectByType<Enigme_GameManager>().CheckValidity();


        ChangeImage();
    }

    private void ChangeImage()
    {
        rend.sprite = visuels[position];
    }
}
