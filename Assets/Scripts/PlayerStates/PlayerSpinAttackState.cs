using System.Collections;
using UnityEngine;

public class PlayerSpinAttackState : PlayerBaseState
{
    const float DEGREES_IN_A_QUARTER_CIRCLE = 90f;
    const float QUARTERS_IN_A_CIRCLE = 4f;

    // Start is called before the first frame update
    public override void Start(Transform _player)
    {
        base.Start(_player);
        sword.enabled = true;
        currentState = PlayerState.chargeSword;
        stateMachine.RunCoroutine(ExecuteSpinAttack());
    }

    public override void Update()
    {
        if(Input.GetButtonUp(BButton))
        {
            currentState = PlayerState.walk;
        }
    }

    //rotate player transform once during a set amount of time
    private IEnumerator ExecuteSpinAttack()
    {
        float timeElapsed;
        Quaternion startRotation, targetRotation;

        //rotate a quarter circle four times
        //i know it's trash but rotating a full circle once doesn't work
        for (int i = 0; i < QUARTERS_IN_A_CIRCLE; i++)
        {
            timeElapsed = 0;
            startRotation = player.rotation;
            targetRotation = player.rotation * Quaternion.Euler(0, 0, DEGREES_IN_A_QUARTER_CIRCLE);

            while (timeElapsed < stateMachine.data.spinAttackDuration / QUARTERS_IN_A_CIRCLE)
            {
                player.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / stateMachine.data.spinAttackDuration * QUARTERS_IN_A_CIRCLE);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            player.rotation = targetRotation;
        }

        stateMachine.ChangeState(currentState);
    }
}
