using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_Sayer : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBubble;

    private void Start()
    {
        dialogueBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueBubble.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueBubble.SetActive(false);
    }
}
