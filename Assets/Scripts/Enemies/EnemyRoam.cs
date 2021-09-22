using System.Collections;
using UnityEngine;

public class EnemyRoam : Enemy
{
    [SerializeField] private float roamRadius;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float minWaitTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(SetTargetPosition());
    }

    private void FixedUpdate()
    {
        if(currentState == EnemyState.roam)
        {
            if (Vector2.Distance(transform.position, targetPos) > 0.01f)
            {
                Move();
            }
            else
            {
                StartCoroutine(SetTargetPosition());
            }
        }
    }

    IEnumerator SetTargetPosition()
    {
        currentState = EnemyState.idle;

        //wait random time
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

        //set new position
        targetPos = homePos + roamRadius * Random.insideUnitCircle;

        currentState = EnemyState.roam;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(SetTargetPosition());
    }
}
