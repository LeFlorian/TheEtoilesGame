using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumaVisualController : MonoBehaviour
{
    public int lumaID;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetInteger("LumaID", lumaID);
    }
}
