using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.AsyncOperations;

public class Intro : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint = new Vector3(0f,4.5f,-20f);
    public float speed = 1f;

    public Transform cam;
    private float timer;
    private bool _start = false;
    private Vector3 diff;

    void Start(){
        startPoint = cam.position;
        diff = endPoint-startPoint;
    }

    void Update(){
        if (timer <= speed ) {
            timer += Time.deltaTime;
			float percent = timer / speed;
			cam.position = startPoint + diff * percent;
        }
        if (! _start)
        {
            _start = true;
            ChartReader chart = GetComponent<ChartReader>();
            chart.StartMusic(0f);
        }
    }
}