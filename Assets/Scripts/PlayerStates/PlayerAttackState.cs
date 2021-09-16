using System.Collections;
using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerState nextState;

    public override void Start(GameObject _player)
    {
        base.Start(_player);

        nextState = PlayerState.chargeSword;

        //ask StateMachine kindly to run the attack coroutine for us
        stateMachine.RunCoroutine(Attack());
    }

    public override void Update()
    {
        //if player held the B button through their entire attack they go to charge sword state
        //if they released the button they return to walk state
        if(Input.GetButtonUp(Constants.BButton))
        {
            nextState = PlayerState.walk;
        }
    }

    private IEnumerator Attack()
    {
        //execute attack and move to next state
        yield return new WaitForSeconds(Constants.attackDuration);
        stateMachine.ChangeState(nextState);
    }
}