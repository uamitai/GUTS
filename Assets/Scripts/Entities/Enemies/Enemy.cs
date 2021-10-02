//derived from entity, this is the common behavior of all enemies in the game

using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    protected Vector2 homePos;
    protected Vector2 targetPos;
    protected bool isIdle;
    protected const string playerTag = "Player";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        homePos = transform.position;
        targetPos = transform.position;
        isIdle = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(playerTag) && collision.isTrigger && !isInvulnerable)
        {
            //damage player
            collision.gameObject.GetComponent<Player>().TakeHit(transform.position, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    protected void MoveTo(Vector2 target)
    {
        if (isIdle) { return; }

        //move towards target position
        transform.position =  Vector2.MoveTowards(transform.position, target, data.moveSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime));
    }

    protected float DistanceFrom(Vector2 target)
    {
        //return the distance between current position and given pos
        return Vector2.Distance(transform.position, target);
    }

    protected override IEnumerator Knockback(Vector2 direction)
    {
        isIdle = true;
        yield return base.Knockback(direction);
        isIdle = false;
    }
}
