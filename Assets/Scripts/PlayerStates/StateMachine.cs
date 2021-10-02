using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    recover,
    chargeSword,
    sideJump,
    spinAttack,
    lungeAttack,
    jumpAttack
}

public class StateMachine : MonoBehaviour
{
    public PlayerData data;
    private Dictionary<PlayerState, PlayerBaseState> states = new Dictionary<PlayerState, PlayerBaseState>();
    private PlayerBaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        //add states to dict
        states.Add(PlayerState.idle, new PlayerBaseState());
        states.Add(PlayerState.walk, new PlayerWalkState());
        states.Add(PlayerState.attack, new PlayerAttackState());
        states.Add(PlayerState.recover, new PlayerRecoverykState());
        states.Add(PlayerState.spinAttack, new PlayerSpinAttackState());
        states.Add(PlayerState.chargeSword, new PlayerChargeSwordState());
        states.Add(PlayerState.lungeAttack, new PlayerLungeAttackState());

        //defaultly start with the walk state
        ChangeState(PlayerState.walk);
    }

    // Update is called once per frame
    void Update()
    {
        //update current state
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sword hit something
        currentState.OnSword(collision.gameObject, collision);
    }

    //change to new state and start it
    public void ChangeState(PlayerState state)
    {
        if(currentState == states[state])
        {
            return;
        }

        //Debug.Log(state);
        currentState = states[state];
        currentState.Start(transform);
    }

    //run coroutines for player states
    public void RunCoroutine(IEnumerator ienumerator)
    {
        StartCoroutine(ienumerator);
    }
}
