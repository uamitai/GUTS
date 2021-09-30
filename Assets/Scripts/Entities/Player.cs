//player script that derives from entity behavior

using System.Collections;
using UnityEngine;

public class Player : Entity
{
    private const string enemyTag = "Enemy";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            StartCoroutine(EntityCollision());
        }
    }

    //when colliding with another entity, freeze constraints for one frame then carry on
    private IEnumerator EntityCollision()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return null;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected override IEnumerator Knockback(Vector2 direction)
    {
        Debug.Log("ai fuck");
        StateMachine sm = transform.GetChild(0).GetComponent<StateMachine>();
        sm.ChangeState(PlayerState.idle);

        yield return base.Knockback(direction);

        sm.ChangeState(PlayerState.walk);
    }
}
