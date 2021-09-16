using UnityEngine;

//base state class for all player states
public class State
{
    protected GameObject player;
    protected StateMachine stateMachine;

    // Start is called before the first frame update
    public virtual void Start(GameObject _player)
    {
        player = _player;
        stateMachine = player.GetComponent<StateMachine>();
    }

    // Update is called once per frame
    public virtual void Update() { }

    public virtual void FixedUpdate() { }
}
