using System.Collections;
using UnityEngine;

public class PlayerSpinAttackState : PlayerBaseState
{
    // Start is called before the first frame update
    public override void Start(GameObject _player)
    {
        base.Start(_player);
        sword.enabled = true;
        currentState = PlayerState.chargeSword;
        stateMachine.RunCoroutine(ExecuteSpinAttack());
    }

    public override void Update()
    {
        if(Input.GetButtonUp(Constants.BButton))
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator ExecuteSpinAttack()
    {
        yield return new WaitForSeconds(Constants.spinAttackDuration);
        stateMachine.ChangeState(currentState);
    }
}
