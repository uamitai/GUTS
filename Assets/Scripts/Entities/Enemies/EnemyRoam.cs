//enemy that roams inside a circle

using System.Collections;
using UnityEngine;

public class EnemyRoam : Enemy
{
    [SerializeField] private float roamRadius;
    [SerializeField] private float maxWaitTime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(SetTargetPosition());
    }

    private void FixedUpdate()
    {
        if(!isIdle)
        {
            if (DistanceFrom(targetPos) > 0.01f)
            {
                MoveTo(targetPos);
            }
            else
            {
                //Debug.Log("reached target");
                StartCoroutine(SetTargetPosition());
            }
        }
    }

    IEnumerator SetTargetPosition()
    {
        isIdle = true;

        //set new position
        targetPos = homePos + roamRadius * Random.insideUnitCircle;

        //wait random time
        yield return new WaitForSeconds(Random.value * maxWaitTime);

        isIdle = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!isIdle)
        {
            //Debug.Log("hit wall");
            StartCoroutine(SetTargetPosition());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(homePos, roamRadius);
    }
}
