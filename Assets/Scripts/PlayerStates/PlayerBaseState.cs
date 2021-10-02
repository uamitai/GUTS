using System.Collections;
using UnityEngine;

//base state class for all player states
public class PlayerBaseState
{
    //input axis, button and animation parameter names
    protected const string LeftStickHorizontal = "Left_Stick_Horizontal";
    protected const string LeftStickVertical = "Left_Stick_Vertical";
    protected const string BButton = "B_Button";
    protected const string ZLTrigger = "ZL_Trigger";
    protected const string animatorXParameter = "X";
    protected const string animatorYParameter = "Y";

    protected Transform player;
    protected Rigidbody2D rb;
    protected Collider2D sword;
    protected Animator animator;
    protected StateMachine stateMachine;
    protected PlayerState currentState;

    private const string enemyTag = "Enemy";

    // Start is called before the first frame update
    public virtual void Start(Transform _player)
    {
        player = _player;
        rb = player.GetComponentInParent<Rigidbody2D>();
        sword = player.GetComponent<Collider2D>();
        animator = player.GetComponentInParent<Animator>();
        stateMachine = player.GetComponent<StateMachine>();
    }

    // Update is called once per frame
    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public void OnSword(GameObject enemy, Collider2D collision)
    {
        rb.velocity = Vector2.zero;
        if (enemy.CompareTag(enemyTag) && collision.isTrigger)
        {
            enemy.GetComponent<Enemy>().TakeHit(player.parent.position, 0);
        }
    }
}
