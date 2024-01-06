using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingFanArt : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonActiveFanArt;

    private void Update()
    {
        bool isAnyActive = false;

        foreach (Image img in gameObject.GetComponentsInChildren<Image>())
        {
            if (img.GetComponent<Button>() == null)
            {
                if (img.gameObject.activeSelf)
                {

                    isAnyActive = true;
                    _buttonActiveFanArt.SetActive(true);
                }
            }

        }

        if (!isAnyActive)
        {
            _buttonActiveFanArt.SetActive(false);
        }
    }

    public void DesactivateAllImage()
    {
        foreach (Image img in gameObject.GetComponentsInChildren<Image>())
        {
            if (img.GetComponent<Button>() == null)
            {
                img.gameObject.SetActive(false);
            }

        }
    }
}
