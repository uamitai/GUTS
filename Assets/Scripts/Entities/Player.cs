//player script that derives from entity behavior

using System.Collections;
using UnityEngine;

public class Player : Entity
{
    protected override IEnumerator Knockback(Vector2 direction)
    {
        Debug.Log("owowow");
        StateMachine sm = transform.GetChild(0).GetComponent<StateMachine>();
        sm.StopAllCoroutines();
        sm.ChangeState(PlayerState.idle);

        yield return base.Knockback(direction);

        sm.ChangeState(PlayerState.walk);
    }
}
