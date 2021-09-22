using UnityEngine;

public enum EnemyState
{
    idle,
    roam,
    patrol,
    chase
}

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int attack;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Vector2 homePos;
    [SerializeField] protected Vector2 targetPos;

    protected Rigidbody2D rb;
    protected EnemyState currentState;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        homePos = transform.position;
        targetPos = transform.position;
    }

    protected virtual void Move()
    {
        //move towards target position
        rb.MovePosition(Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime));
    }
}
