using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 10.0f;

    // ------------------------------
    // Private variables
    // ------------------------------
    Vector3 mMovement;
    Animator mAnim;
    Rigidbody mRigidBody;
    Vector3 mTargetPosition;          // Target postion for the player
    Quaternion mTargetOrientation;    // Target orientation for the player
    int mFloorRayMask;                // Floor layer for ray-casting
    float mCurrMoveSpeed;             // Current move speed for the player
    float mSlowMoveTimer = 0.0f;      // Timer for when the player is hit by a blast
    bool mGameDone = false;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start ()
    {
        mFloorRayMask = LayerMask.GetMask("Floor");
        mAnim = GetComponent<Animator>();
        mRigidBody = GetComponent<Rigidbody>();

        mTargetPosition = transform.position;
        mTargetOrientation = transform.rotation;

        mCurrMoveSpeed = moveSpeed;
        mAnim.SetFloat("MoveSpeed", mCurrMoveSpeed);
    }

    // ----------------------------------------------------------
    // VirtualFunction: Use FixedUpdate for physics functions
    // ----------------------------------------------------------
    // Using FixedUpdate for the movement gives smoother results
    // than using the Update function
    void FixedUpdate ()
    {
        if (!mGameDone)
        {
            // Raycast from the camera to see if the player target location should be updated
            TestRayCast();

            // Update the slow move timer and update player speed if necessary
            if (mSlowMoveTimer > 0f)
            {
                mSlowMoveTimer -= Time.deltaTime;
            }
            else
            {
                mSlowMoveTimer = 0f;
                mCurrMoveSpeed = moveSpeed;
                mAnim.SetFloat("MoveSpeed", mCurrMoveSpeed);
            }

            // Rotate the player model
            Rotate();

            // Move the player if the target location is not the players current location
            if (transform.position != mTargetPosition)
            {
                //mCurrMoveSpeed = moveSpeed;

                // Move the player model
                Move();

                // Animate the player
                Animate();

                // Check the distance against the error amount
                if ((transform.position - mTargetPosition).magnitude < 0.25f)
                {
                    transform.position = mTargetPosition;
                }
            }
            else
            {
                mAnim.SetBool("IsMoving", false);
            }
        }
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update ()
    {

    }

    // ------------------------------------------------------------------------
    // Test to see if there is a new target location for the player
    // ------------------------------------------------------------------------
    void TestRayCast()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Check to see that the mouse button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Perform the raycast and check if it hit anything on the floor layer
            if (Physics.Raycast(camRay, out floorHit, 100.0f, mFloorRayMask))
            {
                // Set the target position
                mTargetPosition = floorHit.point;
                mTargetPosition.y = 0.0f;

                // Get the look-at vector to set the players target orientation
                Vector3 lookAt = mTargetPosition - transform.position;

                // Set the quaternion (rotation) based on looking down the vector from the player to the mouse.
                mTargetOrientation = Quaternion.LookRotation(lookAt);

                //Debug.Log("CurrPosition = " + transform.position + " TarPosition = " + mTargetPosition.ToString());
                //Debug.Log("CurrRotation = " + transform.rotation + " TarRotation = " + mTargetOrientation.ToString());
            }
        }
    }

    // ------------------------------------------------------------------------
    // Rotate the player to the targetOrientation
    // ------------------------------------------------------------------------
    void Rotate()
    {
        // Set the player's rotation to this new rotation.
        mRigidBody.MoveRotation(mTargetOrientation);
        //mRigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, mTargetOrientation, Time.deltaTime * rotateSpeed));
    }

    // ------------------------------------------------------------------------
    // Move the player to the targetPosition
    // ------------------------------------------------------------------------
    void Move()
    {
        // Get the distance from the current position to the target position
        Vector3 moveDist = mTargetPosition - transform.position;
        
        // Normalise the movement vector and make it proportional to the speed per second.
        moveDist = moveDist.normalized * mCurrMoveSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        mRigidBody.MovePosition(transform.position + moveDist);
    }

    // --------------------------------------------------
    // Animate the player
    // --------------------------------------------------
    void Animate()
    {
        mAnim.SetBool("IsMoving", true);
    }

    // --------------------------------------------------
    // Called if the player enters a collider trigger (ie. collectible)
    // --------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        Collectible coll = other.GetComponent<Collectible>();
        CollectibleExplosion exp = other.GetComponent<CollectibleExplosion>();

        //Debug.Log(exp);
       // Debug.Log(coll);

        if (exp && exp.IsExploding())
        {
            if (mSlowMoveTimer <= 0f)
              mCurrMoveSpeed /= 2f;
            mSlowMoveTimer = 3f;
            mAnim.SetFloat("MoveSpeed", mCurrMoveSpeed);
        }

        if (coll && !coll.IsExploding())
        {
            int pointVal = coll.GetPointVal();
            SendMessage("UpdateScore", pointVal);

            // Play the pickup sound
            GameObject.Find("PickupSound").GetComponent<AudioSource>().Play();

            Destroy(other.gameObject);
        }
    }

    // --------------------------------------------------
    // Called if the player is still in a collider trigger
    // --------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CollectibleExplosion>())
        {
            CollectibleExplosion exp = other.GetComponent<CollectibleExplosion>();

            //Debug.Log(exp);
            //Debug.Log(exp.IsExploding());

            if (exp.IsExploding())
            {
                if (mSlowMoveTimer <= 0f)
                  mCurrMoveSpeed /= 2f;
                mSlowMoveTimer = 3f;
                mAnim.SetFloat("MoveSpeed", mCurrMoveSpeed);
            }
        }
    }

    // --------------------------------------------------
    // Called when the game timer reaches 0 (the game is over)
    // --------------------------------------------------
    void GameOver()
    {
        mGameDone = true;
        mAnim.SetBool("IsMoving", false);
    }

}
