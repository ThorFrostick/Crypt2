using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    /* - Thanks to Dave / GameDevelopment: https://www.youtube.com/watch?v=f473C43s8nE - */

    //Store the sensitivity in the X and Y axes.
    public float xSense;
    public float ySense;

    //Get the orientation of the player so we can update the forward direction.
    public Transform orientation;

    //Store the rotation for the X and Y axes.
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;

        //Set the cursor to be invisible
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the mouse movement along the x-axis, scaling it by Delta Time and the sensitivity.
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSense;

        //Get the mouse movement along the y-axis, scaling it by Delta Time and the sensitivity.
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySense;

        //
        yRotation += mouseX;
        xRotation -= mouseY;

        //Prevent the camera from moving up and down more than 90 degrees.
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Apply the rotation to the transform
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //Apply a second rotation to the orientation to change what direction is forward.
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    /* --------------------------------------------------------------------------------- */
}
