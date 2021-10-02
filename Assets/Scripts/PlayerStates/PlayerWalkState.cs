using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    private Vector2 vel;

    public override void Start(Transform _player)
    {
        base.Start(_player);
        vel = Vector2.zero;
        sword.enabled = false;
    }

    public override void Update()
    {
        //go to other states if necessary
        if(Input.GetButtonDown(BButton))
        {
            stateMachine.ChangeState(PlayerState.attack);
        }
        if(Input.GetButtonDown(ZLTrigger))
        {
            stateMachine.ChangeState(PlayerState.recover);
        }

        //calculate velocity based on input
        vel = new Vector2(
            Input.GetAxisRaw(LeftStickHorizontal),
            Input.GetAxisRaw(LeftStickVertical)
            );
        //Debug.Log(vel.magnitude);
    }

    public override void FixedUpdate()
    {
        if(vel.magnitude > stateMachine.data.moveThreshhold)
        {
            //move player
            //x = x0 + v * dt
            rb.MovePosition(rb.position + vel.normalized * stateMachine.data.moveSpeed * Time.fixedDeltaTime);

            //rotate player
            player.transform.up = -vel.normalized;

            //set animation parameters
            animator.SetFloat(animatorXParameter, vel.x);
            animator.SetFloat(animatorYParameter, vel.y);
        }
    }
}