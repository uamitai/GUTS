using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public override void Start(Transform _player)
    {
        base.Start(_player);
        currentState = PlayerState.chargeSword;

        //activate sword collider
        sword.enabled = true;

        //ask StateMachine kindly to run the attack coroutine for us
        stateMachine.RunCoroutine(Attack());
    }

    public override void Update()
    {
        if(Input.GetButtonUp(BButton))
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(stateMachine.data.attackDuration);

        //if B button is kept held down player starts charging sword, else return to walk state
        stateMachine.ChangeState(currentState);
    }
}