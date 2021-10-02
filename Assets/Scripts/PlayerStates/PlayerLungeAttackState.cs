using System.Collections;
using UnityEngine;

public class PlayerLungeAttackState : PlayerBaseState
{
    // Start is called before the first frame update
    public override void Start(Transform _player)
    {
        base.Start(_player);
        sword.enabled = true;
        currentState = PlayerState.lungeAttack;
        stateMachine.RunCoroutine(ExecuteLungeAttack());
    }

    // Update is called once per frame
    public override void Update()
    {
        if(currentState != PlayerState.lungeAttack)
        {
            return;
        }

        //on B button press do jump attack
        if(Input.GetButtonDown(BButton))
        {
            stateMachine.StopAllCoroutines();
            rb.velocity = Vector2.zero;
            stateMachine.RunCoroutine(ExecuteJumpAttack());
        }

        //on ZL trigger press finish lunging then recover
        if(Input.GetButtonDown(ZLTrigger))
        {
            currentState = PlayerState.recover;
        }
    }

    private IEnumerator ExecuteLungeAttack()
    {
        //setup
        currentState = PlayerState.lungeAttack;
        rb.velocity = -player.transform.up * stateMachine.data.lungeAttackVelocity;

        //wait duration
        yield return new WaitForSeconds(stateMachine.data.lungeAttackDuration);

        //stop
        rb.velocity = Vector2.zero;

        //change state
        currentState = currentState == PlayerState.recover ? PlayerState.recover : PlayerState.walk;
        stateMachine.ChangeState(currentState);
    }

    private IEnumerator ExecuteJumpAttack()
    {
        //prepare to jump
        //Debug.Log("jumpAttack");
        currentState = PlayerState.jumpAttack;
        sword.enabled = false;
        yield return new WaitForSeconds(stateMachine.data.jumpAttackPrepTime);

        //jump
        sword.enabled = true;
        rb.velocity = -player.transform.up * stateMachine.data.jumpAttackVelocity;
        yield return new WaitForSeconds(stateMachine.data.jumpAttackDuration);

        //stop
        //sword.enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stateMachine.data.jumpAttackCooldown);
        stateMachine.ChangeState(PlayerState.walk);
    }
}
