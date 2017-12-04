using UnityEngine;
using System.Collections;

public enum CollectibleType
{
    Bouncy,
    ColorChanging,
    Bomb
};

public class Collectible : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public float totalDuration = 10f;
    public float blinkDuration = 3f;
    public float blinkSpeed = 10f;

    // ------------------------------
    // Private variables
    // ------------------------------
    float mStartTime;
    CollectibleType mCurrType;
    int mPointVal;
    Color mCurrColor;
    Color mBlinkColor;
    float mBlinkTime = 0f;
    float mColorChangeTime = 0f;
    bool mBlowingUp = false;
    bool expTrigger = false;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        // Get a random type of collectible
        mCurrType = (CollectibleType)Mathf.Floor(Random.Range(0, 3));

        Collider coll = GetComponent<Collider>();
        Renderer rend = GetComponent<Renderer>();
        Light light = GetComponent<Light>();
        PhysicMaterial physMat = new PhysicMaterial();

        switch (mCurrType)
        {
            case CollectibleType.Bouncy:
                //physMat.bounciness = 1f;
                mCurrColor = Color.green;
                light.range = 5f;
                mPointVal = 5;
                break;

            case CollectibleType.ColorChanging:
                physMat.bounciness = 0.6f;
                coll.material = physMat;
                mCurrColor = Color.yellow;
                light.range = 8f;
                mPointVal = 3;
                break;

            case CollectibleType.Bomb:
                physMat.bounciness = 0.4f;
                coll.material = physMat;
                mCurrColor = Color.red;
                light.range = 12f;
                totalDuration--;
                mPointVal = 8;
                break;

            default:
                mPointVal = 5;
                break;
        }

        // Set parameters for the collectible
        //coll.material = physMat;
        rend.material.SetColor("_Color", mCurrColor);
        light.color = mCurrColor;

        mStartTime = Time.time;
        mBlinkColor = mCurrColor;
        mBlinkColor.a = 0f;

        BroadcastMessage("Initialize", mCurrType);

        //Destroy(physMat);
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {
        float elapsedTime = Time.time - mStartTime;

        // Collectible has reached its duration so destroy it
        if (elapsedTime >= totalDuration)
        {
            Destroy(gameObject);
        }
        // Collectible has reached its blink time so start blinking
        else if (elapsedTime >= (totalDuration - blinkDuration))
        {
            mBlinkTime = Mathf.Cos(Time.time * blinkSpeed);

            Renderer rend = GetComponent<Renderer>();
            Color tempColor = Color.Lerp(mCurrColor, mBlinkColor, mBlinkTime);
            rend.material.SetColor("_Color", tempColor);

            if (elapsedTime >= (totalDuration - 1f))
            {
                if (!expTrigger)
                {
                    expTrigger = true;

                    BroadcastMessage("PlayExplosion", mCurrColor);
                    mBlowingUp = true;
                }
            }
        }
        else if (mCurrType == CollectibleType.ColorChanging)
        {
            mColorChangeTime += Time.deltaTime;

            if (mColorChangeTime >= 1f)
            {
                mColorChangeTime -= 1f;

                Renderer rend = GetComponent<Renderer>();
                Light light = GetComponent<Light>();
                mCurrColor.r = Random.value;
                mCurrColor.g = Random.value;
                mCurrColor.b = Random.value;
                rend.material.SetColor("_Color", mCurrColor);
                light.color = mCurrColor;
            }
        }
    }

    // --------------------------------------------------
    // Returns the point value for the collectible
    // --------------------------------------------------
    public int GetPointVal()
    {
        return mPointVal;
    }

    // --------------------------------------------------
    // Returns if the collectible is exploding or not
    // --------------------------------------------------
    public bool IsExploding()
    {
        return mBlowingUp;
    }
}
