using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugLee : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<GameControllerGeography>().canRespawn == false)
        {
            animator.SetBool("PlayerDeath", true);
        }
        else {
            animator.SetBool("PlayerDeath", false);
        }
    }
}
