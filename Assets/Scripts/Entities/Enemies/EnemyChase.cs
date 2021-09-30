//enemy that chases the player when they get close

using UnityEngine;

public class EnemyChase : Enemy
{
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;

    private Transform target;
    private float distance;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentState = EnemyState.walk;
        target = GameObject.FindWithTag(playerTag).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentState == EnemyState.walk)
        {
            targetPos = target.position;
            distance = DistanceFrom(targetPos);
            if(attackRadius < distance && distance < chaseRadius)
            {
                MoveTo(targetPos);
            }
        }
    }
}
