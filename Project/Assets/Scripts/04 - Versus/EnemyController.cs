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
    [SerializeField]
    private float attackForce;
    [SerializeField]
    private float attackDeacrease;

    [SerializeField]
    private float delayBetweenTwoAttacks;
    private bool asAttack;
    private float delay;

    [SerializeField]
    private float damageTaken;
    [SerializeField]
    private float damageTakenDecrease;
    [SerializeField]
    private float gravityWhenPunch;
    [SerializeField]
    private float timeToReturnToBaseGravity;

    private float baseGravity;

    private Vector2 hitVelocity;

    private PlayerController pc;
    private Rigidbody2D rb;

    private void Start()
    {
        baseGravity = gravity;
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
        velocity += hitVelocity;

        rb.AddForce(velocity);

        transform.rotation = Quaternion.identity;


        if (Vector2.Distance(pc.transform.position, this.transform.position) <= attackPlayerDistance)
        {
            if (!asAttack)
            {
                asAttack = true;
                Vector2 direction = (Vector2)(pc.transform.position - this.transform.position).normalized;

                pc.HitingPlayer(direction, attackForce, attackDeacrease);
            }
        }
    }

    private void Update()
    {
        if (asAttack)
        {
            delay += Time.deltaTime;

            if (delay >= delayBetweenTwoAttacks)
            {
                asAttack = false;
                delay = 0;
            }
        }
    }

    public void TakeDamage()
    {
        Vector2 direction = (Vector2)(pc.transform.position - this.transform.position).normalized;
        Hiting(-direction, damageTaken, damageTakenDecrease);

        StartCoroutine(GravityScaling());
    }

    private void Hiting(Vector2 dir, float force, float deacreseForce)
    {
        force = force + GetComponent<DamageVersus>().AddingDamage() * force;

        StartCoroutine(ExplosionHit(dir, force, deacreseForce));
    }

    private IEnumerator ExplosionHit(Vector2 dir, float force, float deacreseForce)
    {
        hitVelocity += dir * force;
        yield return new WaitForFixedUpdate();
        float newForce = force - deacreseForce * Time.fixedDeltaTime;

        if (newForce > 0f)
        {
            StartCoroutine(ExplosionHit(dir, newForce, deacreseForce));
        }
        else
        {
            hitVelocity = Vector2.zero;
        }
    }

    private IEnumerator GravityScaling()
    {
        float time = 0;

        while (time < timeToReturnToBaseGravity)
        {
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;

            float timeUnlerp = Mathf.InverseLerp(0, timeToReturnToBaseGravity, time);
            float lerpGravity = Mathf.Lerp(gravityWhenPunch, baseGravity, timeUnlerp);

            gravity = lerpGravity;
        }

        gravity = baseGravity;
    }
}
