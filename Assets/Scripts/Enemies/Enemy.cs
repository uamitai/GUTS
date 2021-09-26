//derived from entity, this is the common behavior of all enemies in the game

using System.Collections;
using UnityEngine;

public enum EnemyState
{
    idle,
    roam,
    patrol,
    chase
}

public class Enemy : Entity
{
    [SerializeField] protected float moveSpeed;

    protected Vector2 homePos;
    protected Vector2 targetPos;
    protected EnemyState currentState;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        homePos = transform.position;
        targetPos = transform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected void MoveTo(Vector2 target)
    {
        //move towards target position
        rb.MovePosition(Vector2.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime));
    }

    protected float DistanceFromTarget()
    {
        //return the distance between current position and given pos
        return Vector2.Distance(transform.position, targetPos);
    }

    protected override IEnumerator EntityCollision()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield break;
    }
}
