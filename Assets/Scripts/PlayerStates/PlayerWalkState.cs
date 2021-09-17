using UnityEngine;

public class PlayerWalkState : State
{
    private Vector2 vel;

    public override void Start(GameObject _player)
    {
        base.Start(_player);
        vel = Vector2.zero;
    }

    public override void Update()
    {
        //go to other states if necessary
        if(Input.GetButtonDown(Constants.BButton))
        {
            stateMachine.ChangeState(PlayerState.attack);
        }
        if(Input.GetButtonDown(Constants.ZLTrigger))
        {
            stateMachine.ChangeState(PlayerState.recover);
        }

        //calculate velocity based on input
        vel = new Vector2(
            Input.GetAxisRaw(Constants.LeftStickVertical),
            -Input.GetAxisRaw(Constants.LeftStickHorizontal)
            );
        //Debug.Log(vel.magnitude);
    }

    public override void FixedUpdate()
    {
        if(vel.magnitude > Constants.moveThreshhold)
        {
            //move player
            //x = x0 + v * dt
            rb.MovePosition(rb.position + vel.normalized * Constants.moveSpeed * Time.fixedDeltaTime);

            //rotate player
            player.transform.up = -vel;
        }
    }
}