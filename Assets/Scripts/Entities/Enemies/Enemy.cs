//derived from entity, this is the common behavior of all enemies in the game

using System.Collections;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack
}

public class Enemy : Entity
{
    [SerializeField] protected float moveSpeed;

    protected Vector2 homePos;
    protected Vector2 targetPos;
    protected EnemyState currentState;
    protected const string playerTag = "Player";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        homePos = transform.position;
        targetPos = transform.position;
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
        if(collision.gameObject.CompareTag(playerTag) && collision.isTrigger)
        {
            //damage player
            collision.gameObject.GetComponent<Player>().TakeHit(transform.position, 0);
        }
    }

    protected void MoveTo(Vector2 target)
    {
        if(currentState == EnemyState.idle)
        {
            return;
        }

        //move towards target position
        transform.position =  Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime));
    }

    protected float DistanceFrom(Vector2 target)
    {
        //return the distance between current position and given pos
        return Vector2.Distance(transform.position, target);
    }

    public void ChangeState(EnemyState state)
    {
        currentState = state;
    }

    protected override IEnumerator Knockback(Vector2 direction)
    {
        currentState = EnemyState.idle;

        yield return base.Knockback(direction);

        currentState = EnemyState.walk;
    }
}
