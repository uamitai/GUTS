using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public override void Start(GameObject _player)
    {
        base.Start(_player);

        //ask StateMachine kindly to run the attack coroutine for us
        stateMachine.RunCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(Constants.attackDuration);

        //if B button is kept held down player starts charging sword, else return to walk state
        stateMachine.ChangeState(Input.GetButton(Constants.BButton) ? PlayerState.chargeSword : PlayerState.walk);
    }
}