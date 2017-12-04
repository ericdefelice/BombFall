using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public Text scoreText;

    // ------------------------------
    // Private variables
    // ------------------------------
    int mScore = 0;
    bool mScoreDirty = false;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        scoreText.text = "Score\n" + mScore.ToString();
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {
        if (mScoreDirty)
        {
            scoreText.text = "Score\n" + mScore.ToString();
            mScoreDirty = false;
        }
    }

    // --------------------------------------------------
    // Update the players score if a collectible was gathered
    // --------------------------------------------------
    void UpdateScore(int pointVal)
    {
        mScore += pointVal;
        mScoreDirty = true;
    }

    // --------------------------------------------------
    // Called when the game timer reaches 0 (the game is over)
    // --------------------------------------------------
    void GameOver()
    {
        PlayerPrefs.SetInt("Score", mScore);
    }
}
