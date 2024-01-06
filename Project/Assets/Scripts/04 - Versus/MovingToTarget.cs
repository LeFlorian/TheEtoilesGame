using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToTarget : MonoBehaviour
{
    private RectTransform target;
    private RectTransform myRectTransform;
    [SerializeField]
    private Vector2 movementSpeedMinMax;
    [SerializeField]
    private Vector2 rotationSpeedMinMax;

    private float movementSpeed;
    private float rotationSpeed;

    private float baseDistToTarget;
    private float lerpedScale;
    [SerializeField]
    private AnimationCurve scaleWithDistance;

    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();

        movementSpeed = Random.Range(movementSpeedMinMax.x, movementSpeedMinMax.y);
        rotationSpeed = Random.Range(rotationSpeedMinMax.x, rotationSpeedMinMax.y);
    }

    private void Update()
    {
        if (target != null)
        {
            myRectTransform.position = myRectTransform.position + (target.position - myRectTransform.position).normalized *Time.deltaTime*movementSpeed;

            myRectTransform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            
            float actualDistance = Vector2.Distance(myRectTransform.position, target.position);
            lerpedScale = Mathf.InverseLerp(0, baseDistToTarget, actualDistance);
            lerpedScale = scaleWithDistance.Evaluate(lerpedScale);
            myRectTransform.localScale = Vector3.one * lerpedScale;

            if (actualDistance <= 0.1f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetTarget(GameObject go)
    {
        target = go.GetComponent<RectTransform>();

        if (myRectTransform == null)
            myRectTransform = GetComponent<RectTransform>();

        baseDistToTarget = Vector2.Distance(myRectTransform.position, target.position);
    }
}
