using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    /* - Thanks to Dave / GameDevelopment: https://www.youtube.com/watch?v=f473C43s8nE - */

    //Set the movement speed in the inspector.
    [Header("Movement")]
    public float moveSpeed;

    //Get the orientation of the player so we can change the forward direction
    public Transform orientation;

    //Determine if the player is on a ground object.
    //Save the height of the player to determine if it is on the ground,
    //the Layer Mask that will tell us if it is ground,
    //and a bool to determine if we must apply drag and friction or not.
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;

    //Determine the force of friction our player recieves while grounded.
    public float groundDrag;

    //Differentiate between Horizontal movement(X and Z axis) and Vertical movement(Y axis).
    float horizontalInput;
    float verticalInput;

    //Calculate a Vector3 that determines movement direction in three dimensions.
    [HideInInspector]
    public Vector3 direction;

    //Save a reference to the rigidbody of the Player so we can apply movement to the player object.
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody of the component this script is attached to.
        rb = GetComponent<Rigidbody>();

        //Freeze the rotation so the player doesn't fall over(this won't interfere with mouse rotation).
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Determine if the player is grounded(on the ground) or not using a Raycast.
        //First, get the position of the player, then look directly downward.
        //If the isGround object is within 70% the player's height(from the player's center), return true.
        //If the ground is outside that range, return false.
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);
        
        MyInput();
        SpeedControl();

        //If the player is grounded, apply drag force to the player.
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        //If the player is not grounded(in the air), do not apply drag to them.
        else
        {
            rb.drag = 0;
        }
    }

    /// <summary>
    /// Move the player at a fixed rate.
    /// </summary>
    private void FixedUpdate()
    {
        MovePlayer();
    }

    /// <summary>
    /// Get the Horizontal and Vertical movement of the user input.
    /// </summary>
    private void MyInput()
    {
        //Get the movement in the horizontal(X and Z) and vertical(Y) axes.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Get the direction based on input and apply movement to the player in that direction.
    /// </summary>
    private void MovePlayer()
    {
        //Calculate the movement direction vector that has vertical and horizontal inputs taken in,
        //as well as forcing the movement in the direction the player is facing.
        //direction = transform.forward * verticalInput + transform.right * horizontalInput;
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Apply this movement direction as a force to the player object through the rigidbody.
        //Normalize the vector to get just the direction out of it, then scale it by the movement speed,
        //and then scale that by 10 simply to make our player move faster.
        rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    /// <summary>
    /// Restrict the player from going beyond the max velocity set by the "moveSpeed" field.
    /// </summary>
    private void SpeedControl()
    {
        //Get the flat velocity of the player, which is just the X and Z axis direction.
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Restrict the velocity from going beyond the set max speed.
        if(flatVel.magnitude > moveSpeed)
        {
            //To do this, get just the direction of the flat velocity and scale it by the max speed
            Vector3 limitedVel = flatVel.normalized * moveSpeed;

            //Finally, apply this limited max velocity to the player.
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    /* --------------------------------------------------------------------------------- */
}
