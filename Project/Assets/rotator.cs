using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class rotator : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(Vector3.forward*speed*Time.deltaTime);
    }
}
