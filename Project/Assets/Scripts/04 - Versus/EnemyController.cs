using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float forceToGoToThePlayer;
    [SerializeField]
    private float gravity;
    private float gravityAmount;

    [SerializeField]
    private float attackPlayerDistance;

    private PlayerController pc;
    private Rigidbody2D rb;

    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float fdt = Time.fixedDeltaTime;

        Vector2 velocity = Vector2.zero;
        Vector2 directionToPlayer = pc.transform.position - transform.position;
        directionToPlayer = new Vector2(directionToPlayer.x,0).normalized;

        velocity += directionToPlayer * forceToGoToThePlayer;

        RaycastHit2D floorHit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y/2 + 0.1f);
        Debug.Log(floorHit.collider);

        if (floorHit.collider != null && floorHit.collider.tag != this.gameObject.tag)
        {
            gravityAmount = 0.1f;
        }
        else
        {
            gravityAmount += gravity * fdt;
        }

        velocity += Vector2.down * gravityAmount;

        rb.velocity = velocity;


        if (Vector2.Distance(pc.transform.position, this.transform.position) <= attackPlayerDistance)
        {
            pc.GetComponent<Rigidbody2D>().velocity = velocity + (Vector2)(pc.transform.position - this.transform.position).normalized * 100;
        }
    }

    public void TakeDamage()
    {

    }
}
