using System.Collections;
using UnityEngine;

public class PlayerRecoverykState : PlayerBaseState
{
    public override void Start(Transform _player)
    {
        base.Start(_player);
        sword.enabled = false;
        stateMachine.RunCoroutine(ExecuteRecovery());
    }

    public override void Update()
    {
        //on B button do spin attack
        if(Input.GetButtonDown(BButton))
        {
            rb.velocity = Vector2.zero;
            stateMachine.StopAllCoroutines();
            stateMachine.ChangeState(PlayerState.spinAttack);
        }
    }

    private IEnumerator ExecuteRecovery()
    {
        //push player back and return to walk state
        rb.velocity = player.transform.up * stateMachine.data.recoveryVelocity;
        yield return new WaitForSeconds(stateMachine.data.recoveryDuration);

        //start cooldown
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stateMachine.data.recoveryCooldownDuration);
        stateMachine.ChangeState(PlayerState.walk);
    }
}