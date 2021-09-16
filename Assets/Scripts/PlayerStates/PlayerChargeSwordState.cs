using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeSwordState : State
{
    private Vector2 vel;
    private Rigidbody2D rb;
    private PlayerState currentState;
    private float startTime;

    // Start is called before the first frame update
    public override void Start(GameObject _player)
    {
        base.Start(_player);
        vel = Vector2.zero;
        currentState = PlayerState.chargeSword;
        rb = player.GetComponent<Rigidbody2D>();
        startTime = Time.time;
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
            BButtonPressed();
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

        float dotProduct = Vector2.Dot(vel, player.transform.right);
        //Debug.Log(dotProduct);

        //if the dot product is high enough perform a side jump, otherwise go to recovery state
        if(Mathf.Abs(dotProduct) > Constants.sideJumpThreshhold)
        {
            Debug.Log("sideJump");
            currentState = PlayerState.sideJump;
            stateMachine.RunCoroutine(ExecuteSideJump(dotProduct));
        }
        else
        {
            stateMachine.ChangeState(PlayerState.recover);
        }
    }

    private void BButtonPressed()
    {
        //executing side jump
        if(currentState != PlayerState.chargeSword)
        {
            return;
        }

        if(Time.time - startTime > Constants.chargeSwordDuration)
        {
            //lunge!
            stateMachine.RunCoroutine(ExecuteLungeAttack());
        }
        else
        {
            //didn't charge enough
            stateMachine.ChangeState(PlayerState.walk);
        }
    }

    private IEnumerator ExecuteSideJump(float dotProduct)
    {
        //give velocity to jump
        rb.velocity = player.transform.right * Mathf.Sign(dotProduct) * Constants.sideJumpVelocity;

        //wait
        yield return new WaitForSeconds(Constants.sideJumpDuration);

        //return
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Constants.sideJumpCooldownDuration);
        currentState = PlayerState.chargeSword;
    }

    private IEnumerator ExecuteLungeAttack()
    {
        //setup
        Debug.Log("lungeAttack");
        currentState = PlayerState.lungeAttack;
        rb.velocity = -player.transform.up * Constants.lungeVelocity;

        //wait duration
        yield return new WaitForSeconds(Constants.lungeAttackDuration);

        //stop
        rb.velocity = Vector2.zero;
        stateMachine.ChangeState(PlayerState.walk);
    }
}
