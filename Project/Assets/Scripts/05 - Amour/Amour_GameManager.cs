using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amour_GameManager : MonoBehaviour
{
    public int numberOfCollectableObtain;
    public int howManyCollectable;

    public void ObtainCollectable()
    {
        numberOfCollectableObtain++;

        if (numberOfCollectableObtain >= howManyCollectable)
        {
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("YOU WIN");
    }
}
