using UnityEngine;
using System.Collections;

public class CollectibleExplosion : MonoBehaviour
{

    // ------------------------------
    // Public variables
    // ------------------------------
    public float bombRadius = 4f;

    // ------------------------------
    // Private variables
    // ------------------------------
    bool mBlowingUp = false;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
    }

    // --------------------------------------------------
    // Function to initialize the explosion object
    // --------------------------------------------------
    public void Initialize(CollectibleType type)
    {
        ParticleSystem expSys = GetComponent<ParticleSystem>();
        SphereCollider collider = GetComponent<SphereCollider>();

        switch (type)
        {
            case CollectibleType.Bouncy:
                expSys.startColor = Color.green;
                expSys.startSpeed = 1.0f;
                collider.radius = 1.5f;
                break;

            case CollectibleType.ColorChanging:
                expSys.startColor = Color.yellow;
                expSys.startSpeed = 1.5f;
                collider.radius = 2f;
                break;

            case CollectibleType.Bomb:
                expSys.startColor = Color.red;
                expSys.startSpeed = 1.8f;
                collider.radius = 2.5f;
                break;

            default:
                break;
        }
    }

    // --------------------------------------------------
    // Function to play the explosion effect
    // --------------------------------------------------
    public void PlayExplosion(Color expColor)
    {
        ParticleSystem expSys = GetComponent<ParticleSystem>();
        AudioSource expSound = GetComponent<AudioSource>();

        // Start the explosion particle effect
        expSys.startColor = expColor;
        expSys.Play();

        // Play the explosion sound
        expSound.Play();

        mBlowingUp = true;

        Destroy(gameObject, 1f);
    }

    // --------------------------------------------------
    // Returns if the collectible is exploding or not
    // --------------------------------------------------
    public bool IsExploding()
    {
        return mBlowingUp;
    }
}
