using System.Collections;
using UnityEngine;

public class PlayerLungeAttackState : PlayerBaseState
{
    private PlayerState currentState;

    // Start is called before the first frame update
    public override void Start(GameObject _player)
    {
        base.Start(_player);
        currentState = PlayerState.lungeAttack;
        stateMachine.RunCoroutine(ExecuteLungeAttack());
    }

    // Update is called once per frame
    public override void Update()
    {
        //on B button press do jump attack
        if(Input.GetButtonDown(Constants.BButton))
        {
            stateMachine.StopAllCoroutines();
            rb.velocity = Vector2.zero;
            stateMachine.RunCoroutine(ExecuteJumpAttack());
        }

        //on ZL trigger press finish lunging then recover
        if(Input.GetButtonDown(Constants.ZLTrigger) && currentState == PlayerState.lungeAttack)
        {
            currentState = PlayerState.recover;
        }
    }

    private IEnumerator ExecuteLungeAttack()
    {
        //setup
        currentState = PlayerState.lungeAttack;
        rb.velocity = -player.transform.up * Constants.lungeAttackVelocity;

        //wait duration
        yield return new WaitForSeconds(Constants.lungeAttackDuration);

        //stop and change state
        rb.velocity = Vector2.zero;
        if (currentState == PlayerState.recover)
        {
            stateMachine.ChangeState(PlayerState.recover);
        }
        else
        {
            stateMachine.ChangeState(PlayerState.walk);
        }
    }

    private IEnumerator ExecuteJumpAttack()
    {
        //prepare to jump
        currentState = PlayerState.jumpAttack;
        Debug.Log("jumpAttack");
        yield return new WaitForSeconds(Constants.jumpAttackPrepTime);

        //jump
        rb.velocity = -player.transform.up * Constants.jumpAttackVelocity;
        yield return new WaitForSeconds(Constants.jumpAttackDuration);

        //stop
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Constants.jumpAttackCooldown);
        stateMachine.ChangeState(PlayerState.walk);
    }
}