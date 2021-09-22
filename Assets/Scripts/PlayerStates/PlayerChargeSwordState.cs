using System.Collections;
using UnityEngine;

public class PlayerChargeSwordState : PlayerBaseState
{
    private Vector2 vel;
    private PlayerState currentState;
    private float chargeTime;

    // Start is called before the first frame update
    public override void Start(GameObject _player)
    {
        base.Start(_player);

        vel = Vector2.zero;
        currentState = PlayerState.chargeSword;
        chargeTime = Time.time;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(Input.GetButtonDown(Constants.ZLTrigger))
        {
            ZLTriggerPressed();
        }

        if(Input.GetButtonUp(Constants.BButton))
        {
            BButtonReleased();
        }

        //player can walk during the charge sword state
        vel = new Vector2(
            Input.GetAxisRaw(Constants.LeftStickVertical),
            -Input.GetAxisRaw(Constants.LeftStickHorizontal)
            );
    }

    public override void FixedUpdate()
    {
        if (vel.magnitude > Constants.moveThreshhold && currentState == PlayerState.chargeSword)
        {
            //move player
            //x = x0 + v * dt
            rb.MovePosition(rb.position + vel.normalized * Constants.holdSwordWalkSpeed * Time.fixedDeltaTime);
        }
    }

    private void ZLTriggerPressed()
    {
        if(currentState != PlayerState.chargeSword)
        {
            return;
        }

        //the dot product determines the component of the held direction on the left stick above the right vector of the player transform
        float dotProduct = Vector2.Dot(vel, player.transform.right);
        //Debug.Log(dotProduct);

        //if the dot product is high enough perform a side jump, otherwise go to recovery state
        if(Mathf.Abs(dotProduct) > Constants.sideJumpThreshhold)
        {
            stateMachine.RunCoroutine(ExecuteSideJump(Mathf.Sign(dotProduct)));
        }
        else
        {
            stateMachine.ChangeState(PlayerState.recover);
        }
    }

    private void BButtonReleased()
    {
        //while executing side jump
        if(currentState == PlayerState.sideJump)
        {
            //stop player and go to attack state
            rb.velocity = Vector2.zero;
            stateMachine.StopAllCoroutines();
            stateMachine.ChangeState(PlayerState.attack);
        }
        //didn't charge enough
        else if (Time.time - chargeTime < Constants.chargeSwordDuration)
        {
            stateMachine.ChangeState(PlayerState.walk);
        }
        else
        {
            //lunge!
            stateMachine.ChangeState(PlayerState.lungeAttack);
        }
    }

    //direction is either 1 or -1, to determine if player jumps right or left respectively
    private IEnumerator ExecuteSideJump(float direction)
    {
        Debug.Log("sideJump");
        currentState = PlayerState.sideJump;

        //jump in given direction
        rb.velocity = player.transform.right * direction * Constants.sideJumpVelocity;

        //wait jump duration
        yield return new WaitForSeconds(Constants.sideJumpDuration);

        //cooldown and return
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Constants.sideJumpCooldownDuration);
        currentState = PlayerState.chargeSword;
    }
}
