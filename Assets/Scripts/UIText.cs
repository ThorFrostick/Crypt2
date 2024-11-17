using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to a Canvas, TextMeshPro that will display Player information.
/// </summary>
public class UIText : MonoBehaviour
{
    //Get the reference to the player object
    public GameObject player;

    //Save the Rigidbody of the Player
    private Rigidbody rb;

    //Get the child TextMeshPro
    public TMPro.TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody of the Player
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Have the attached TextMeshPro write the Player's velocity(magnitude).
        text.text = "Velocity: " + rb.velocity.magnitude;
    }
}
