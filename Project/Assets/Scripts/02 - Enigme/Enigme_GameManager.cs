using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigme_GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct ElementValidationConfig
    {
        public InteractObject_SteleEnigme stele;
        public int validPosition;
    }

    [System.Serializable]
    public struct Line
    {
        public ElementValidationConfig[] validLineConfig;

        public SpriteRenderer[] validIcon;
    }

    [SerializeField]
    private ElementValidationConfig[] validConfigs;

    [SerializeField]
    private Line[] validLines;

    [SerializeField]
    private GameObject level;

    private void Start()
    {
        CheckValidity();
    }

    public void CheckValidity()
    {
        if (IsAValidArray(validConfigs))
        {
            Win();
        }

        foreach (Line line in validLines)
        {
            if (IsAValidArray(line.validLineConfig))
            {
                foreach (SpriteRenderer s in line.validIcon)
                    s.color = Color.green;
            }
            else
            {
                foreach (SpriteRenderer s in line.validIcon)
                    s.color = Color.red;
            }
        }
    }

    private bool IsAValidArray(ElementValidationConfig[] config)
    {
        bool valid = true;

        foreach (ElementValidationConfig c in config)
        {
            if (c.stele.position != c.validPosition)
            {
                valid = false;
                break;
            }
        }

        return valid;
    }

    private void Win()
    {
        Debug.Log("YOU WIN");
        level.GetComponent<Animator>().SetTrigger("Win");
    }
}
