using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows the Player object to move up stairs and ledges of a certain height.
/// </summary>
public class StairsMovement : MonoBehaviour
{
    /* - Thanks to DawnsCrow Games: https://www.youtube.com/watch?v=DrFk5Q_IwG0 - */

    //Get the reference to the Player's feet node.
    [SerializeField]
    GameObject feet;

    //Get the reference to the Player's knees node.
    [SerializeField]
    GameObject knees;

    //Determine how high the Player can climb; aka the height an object must be for the player to step on it.
    [SerializeField]
    float stepHeight = 0.38f;

    //Determine how high up the player moves to make the stepping animation look smoother.
    [SerializeField]
    float stepSmooth = 0.05f;

    //Get the forward direction of the MovementController attached to the Player.
    [SerializeField]
    private MovementController mController;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set the Player's knees to the step height, which will determine how high the player can step up.
        //feet.transform.position = new Vector3(feet.transform.position.x, stepHeight, feet.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Don't forget to actually call the check.
        StepClimb();
    }

    /// <summary>
    /// Check if the Player can climb up an object and move them up if they can.
    /// </summary>
    private void StepClimb()
    {
        //Create a Raycast for the feet to check if they will come up to an object.
        RaycastHit lower;

        //Check if the feet/lower Raycast is detecting an object a short distance in front of the Player.
        if(Physics.Raycast(feet.transform.position, transform.TransformDirection(mController.direction), out lower, 0.5f))
        {
            //Now, if the feet are detecting a hit, check if the knees are also detecting that hit.
            //Make sure to look farther than the feet, and if we do not detect a hit that means there is space to step up.
            //Additionally, remember to check if it is NOT detecting the hit, otherwise we won't be able to climb.
            RaycastHit higher;
            if(!Physics.Raycast(knees.transform.position, transform.TransformDirection(mController.direction), out higher, 0.8f))
            {
                //Get the rigidbody of the Player.
                Rigidbody rb = this.GetComponent<Rigidbody>();

                //Every frame, apply a slight movement upward to make the step look smooth.
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }

    /* -------------------------------------------------------------------------- */

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(knees.transform.position, knees.transform.forward);
        Gizmos.DrawRay(feet.transform.position, feet.transform.forward);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward);
    }
}
