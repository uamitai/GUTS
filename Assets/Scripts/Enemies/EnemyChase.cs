using UnityEngine;

public class EnemyChase : Enemy
{
    [SerializeField] private float chaseRadius;
    private Transform target;
    const string playerTag = "Player";

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //target = GameObject.FindWithTag(playerTag).transform;
    }

    private void Update()
    {
        //targetPos = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(DistanceFromTarget() < chaseRadius)
        {
            MoveTo(targetPos);
        }
        else
        {
            MoveTo(homePos);
        }
    }
}
