using System.Collections;
using UnityEngine;

public class PlayerRecoverykState : State
{
    public override void Start(GameObject _player)
    {
        base.Start(_player);
        stateMachine.RunCoroutine(ExecuteRecovery());
    }

    public override void Update()
    {
        //on B button do spin attack
        if(Input.GetButtonDown(Constants.BButton))
        {
            rb.velocity = Vector2.zero;
            stateMachine.StopAllCoroutines();
            stateMachine.ChangeState(PlayerState.spinAttack);
        }
    }

    private IEnumerator ExecuteRecovery()
    {
        //push player back and return to walk state
        rb.velocity = player.transform.up * Constants.recoveryVelocity;
        yield return new WaitForSeconds(Constants.recoveryDuration);

        //start cooldown
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(Constants.recoveryCooldownDuration);
        stateMachine.ChangeState(PlayerState.walk);
    }
}