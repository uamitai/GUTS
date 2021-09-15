using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float threshhold;

    private ProController controller;
    private Vector2 vel;

    private void Awake()
    {
        controller = new ProController();

        //trigger input actions
        //set vel to be the value that the joystick returns
        controller.Input.LeftStick.performed += context => vel = context.ReadValue<Vector2>();    //when moving
        controller.Input.LeftStick.canceled += context => vel = Vector2.zero;                     //when stopping
    }

    private void FixedUpdate()
    {
        if(vel.magnitude > threshhold)
        {
            //move player       x = x0 + v * dt
            rb.MovePosition(rb.position + vel * moveSpeed * Time.fixedDeltaTime);
            
            //rotate player
            transform.up = -vel;
        }
        Debug.Log(vel.magnitude);
    }

    //enable\disable input actions
    private void OnEnable()
    {
        controller.Input.Enable();
    }

    private void OnDisable()
    {
        controller.Input.Disable();
    }
}
