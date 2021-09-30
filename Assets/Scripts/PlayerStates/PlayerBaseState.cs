using UnityEngine;

//base state class for all player states
public class PlayerBaseState
{
    protected GameObject player;
    protected Rigidbody2D rb;
    protected Collider2D sword;
    protected Animator animator;
    protected StateMachine stateMachine;
    protected PlayerState currentState;

    // Start is called before the first frame update
    public virtual void Start(GameObject _player)
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

    public void OnSword(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().TakeHit(player.transform.parent.position, 0);
    }
}
