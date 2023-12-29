using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TarodevController;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float offsetX = 1;
    [SerializeField]
    private float offsetY = 1;
    [SerializeField]
    private float offsetYWhenTouchIsPressed = 1;
    [SerializeField]
    private float offsetZ = -1;

    [SerializeField]
    private float speedMovingToTarget;
    [SerializeField]
    private float addingSpeedWithInputsX;
    [SerializeField]
    private float addingSpeedWithVelocityY;

    private PlayerController pc;



    private void Awake()
    {
        pc = GetComponentInParent<PlayerController>();

        //Deparenting camera and player
        transform.parent = null;

        transform.position = pc.transform.position + Vector3.forward*offsetZ;
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;

        Vector2 targetPosition = Vector2.zero;

        Vector2 inputMove = InputManager.instance.move.ReadValue<Vector2>();
        Vector2 velocity = pc.GetComponent<Rigidbody2D>().velocity;

        float calculateOffsetX = inputMove.x * offsetX;


        float unlerpVelocityY = Mathf.InverseLerp(0,-pc._stats.MaxFallSpeed,velocity.y);

        float calculateOffsetY = inputMove.y * offsetYWhenTouchIsPressed + offsetY;

        targetPosition.x = calculateOffsetX + pc.transform.position.x;

        targetPosition.y = calculateOffsetY + pc.transform.position.y;
        
        float calculateSpeed = speedMovingToTarget + addingSpeedWithInputsX * inputMove.magnitude;
        if (velocity.y < 0)
        {
            calculateSpeed += addingSpeedWithVelocityY * Mathf.Abs(velocity.y);
        }

        Vector2 movingDestination = Vector2.Lerp(transform.position, targetPosition, calculateSpeed*fdt);

        transform.position = new Vector3(movingDestination.x,movingDestination.y,offsetZ);
    }
}
