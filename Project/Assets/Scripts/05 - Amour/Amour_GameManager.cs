using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amour_GameManager : MonoBehaviour
{
    public int numberOfCollectableObtain;
    public int howManyCollectable;

    public Animator starFragmentAnim;

    public void ObtainCollectable()
    {
        numberOfCollectableObtain++;

    }

    public void CheckValidity()
    {
        if (numberOfCollectableObtain >= howManyCollectable)
        {
            Win();
        }
    }

    private void Win()
    {
        starFragmentAnim.SetTrigger("Win");
    }
}
