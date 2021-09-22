using System.Collections;
using UnityEngine;

public class PlayerSpinAttackState : PlayerBaseState
{
    // Start is called before the first frame update
    public override void Start(GameObject _player)
    {
        base.Start(_player);
        stateMachine.RunCoroutine(ExecuteSpinAttack());
    }

    private IEnumerator ExecuteSpinAttack()
    {
        yield return new WaitForSeconds(Constants.spinAttackDuration);
        stateMachine.ChangeState(PlayerState.walk);
    }
}
