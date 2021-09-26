using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
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
    private Dictionary<PlayerState, PlayerBaseState> states = new Dictionary<PlayerState, PlayerBaseState>();
    private PlayerBaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        //add states to dict
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

    //change to new state and start it
    public void ChangeState(PlayerState state)
    {
        Debug.Log(state);
        currentState = states[state];
        currentState.Start(gameObject);
    }

    public void RunCoroutine(IEnumerator ienumerator)
    {
        StartCoroutine(ienumerator);
    }
}