using System.Collections;
using UnityEngine;

public class PlayerRecoverykState : State
{
    private Rigidbody2D rb;
    private PlayerState currentState;
    private float time;

    public override void Start(GameObject _player)
    {
        base.Start(_player);

        rb = player.GetComponent<Rigidbody2D>();
        currentState = PlayerState.recover;
        stateMachine.RunCoroutine(Recover());
    }

    public override void Update()
    {
        if(Input.GetButtonDown(Constants.BButton))
        {
            currentState = PlayerState.spinAttack;
        }
    }

    private IEnumerator Recover()
    {
        //push player back and return to walk state
        rb.velocity = player.transform.up * Constants.recoveryVelocity;
        yield return new WaitForSeconds(Constants.recoveryDuration);
        rb.velocity = Vector2.zero;

        //execute spin attack if B button was pressed, start cooldown otherwise
        if(currentState == PlayerState.spinAttack)
        {
            stateMachine.RunCoroutine(ExecuteSpinAttack());
        }
        else
        {
            stateMachine.RunCoroutine(RecoveryCooldown());
        }
    }

    private IEnumerator RecoveryCooldown()
    {
        time = Time.time;
        //wait for cooldown time
        while(Time.time - time < Constants.recoveryCooldownDuration)
        {
            if (Input.GetButtonDown(Constants.BButton))
            {
                //execute spin attack and leave
                stateMachine.RunCoroutine(ExecuteSpinAttack());
                yield break;
            }

            //wait one frame
            yield return null;
        }
        stateMachine.ChangeState(PlayerState.walk);
    }

    private IEnumerator ExecuteSpinAttack()
    {
        Debug.Log("spinAttack");
        yield return new WaitForSeconds(Constants.spinAttackDuration);
        stateMachine.ChangeState(PlayerState.walk);
    }
}