using UnityEngine;

//base state class for all player states
public class PlayerBaseState
{
    protected GameObject player;
    protected Rigidbody2D rb;
    protected Animator animator;
    protected StateMachine stateMachine;

    // Start is called before the first frame update
    public virtual void Start(GameObject _player)
    {
        player = _player;
        rb = player.GetComponentInParent<Rigidbody2D>();
        animator = player.GetComponentInParent<Animator>();
        stateMachine = player.GetComponent<StateMachine>();
    }

    // Update is called once per frame
    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
