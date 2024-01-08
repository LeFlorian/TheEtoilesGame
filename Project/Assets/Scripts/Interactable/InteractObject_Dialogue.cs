using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//?
using TarodevController;

public class InteractObject_Dialogue : InteractObject
{
    [SerializeField]
    private string[] indices;

    [SerializeField]
    private GameObject dialogue;
    [SerializeField]
    private TextMeshProUGUI clue;

    private int numberOfClick = 0;

    public override void Action()
    {
        numberOfClick++;

        if (numberOfClick == 1)
        {
            ShowDialogue();
        }
        else
        {
             ChangeClue();
        }
        
            
    }

    private void ShowDialogue()
    {
        dialogue.SetActive(true);
        ChangeClue();
    }

    private void ChangeClue()
    {
        //itération en boucle :D
        if (numberOfClick % indices.Length == 0) dialogue.SetActive(false);
        else
            dialogue.SetActive(true);
            clue.text = indices[numberOfClick%indices.Length];
    }

    private void Update()
    {
        if (Vector2.Distance(FindAnyObjectByType<PlayerController>().transform.position, transform.position) > 3f && dialogue.active)
        {
            dialogue.SetActive(false);
            numberOfClick = 0;
            clue.text = indices[0];
        }
    }

}
