using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        numberOfClick = Mathf.Clamp(numberOfClick, 0,indices.Length);

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
        clue.text = indices[numberOfClick-1];
    }

}
