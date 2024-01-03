using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToTarget : MonoBehaviour
{
    private RectTransform target;
    private RectTransform myRectTransform;

    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = myRectTransform.position + (myRectTransform.position - target.position)*Time.deltaTime*10f;
        }
    }

    public void SetTarget(RectTransform t)
    {
        target = t;
    }
}
