using UnityEngine;
using System.Collections;

public class CollectibleManager : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public GameObject collectible;     // The collectible prefab to be spawned.
    public float waitTime = 2f;      // How long before a new collectible is created.

    // ------------------------------
    // Private variables
    // ------------------------------

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        // Create a collectible after wait time and continue creating after waiting for the wait time
        InvokeRepeating("CreateCollectible", 0.5f, waitTime);
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {

    }

    // --------------------------------------------------
    // Function to create a random collectible
    // --------------------------------------------------
    void CreateCollectible()
    {
        Vector3 spawnPosition;
        // X and Z can be between -10 to +10
        spawnPosition.x = Random.Range(-9.5f, 9.5f);
        spawnPosition.z = Random.Range(-9.5f, 9.5f);
        // Y is between 3 to 6
        spawnPosition.y = Random.Range(3f, 6f);


        // Create an instance of the collectible prefab at the randomly selected position
        Instantiate(collectible, spawnPosition, Quaternion.identity);
    }

    // --------------------------------------------------
    // Called when the game timer reaches 0 (the game is over)
    // --------------------------------------------------
    void GameOver()
    {
        CancelInvoke("CreateCollectible");
    }
}
