using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    
    // ------------------------------
    // Public variables
    // ------------------------------
    public Transform target;
    public float moveSpeed = 5.0f;

    // ------------------------------
    // Private variables
    // ------------------------------
    Vector3 mDistance;          // The camera distance from the target

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        // Set the initial distance
        mDistance = transform.position - target.position;
    }

    // ----------------------------------------------------------
    // VirtualFunction: Use FixedUpdate for physics functions
    // ----------------------------------------------------------
    // Use FixedUpdate since the player is updated in the same function
    void FixedUpdate()
    {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos = target.position + mDistance;

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPos, moveSpeed * Time.deltaTime);
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {

    }

}
