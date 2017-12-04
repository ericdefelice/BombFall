using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour
{

    // ------------------------------
    // Public variables
    // ------------------------------
    public int gameLengthSec;               // The game length in seconds
    public float sceneTransitionTime = 3f;  // Time before the scoreboard pops up
    public Text timerText;

    // ------------------------------
    // Private variables
    // ------------------------------
    float mTimeElapsed = 0f;
    int mTimerMin;
    int mTimerSec;
    bool mGameDone = false;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        gameLengthSec = PlayerPrefs.GetInt("TimeLimit");

        mTimerMin = (gameLengthSec / 60);
        mTimerSec = (gameLengthSec % 60);

        string timeMinStr = mTimerMin.ToString();
        string timeSecStr = mTimerSec.ToString("00");
        timerText.text = timeMinStr + ":" + timeSecStr;
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {
        // Update the elapsed game time
        mTimeElapsed += Time.deltaTime;

        // If 1 second has passed, update the timer values and text
        if (mTimeElapsed >= 1.0f)
        {
            // Update the elapsed time and the seconds
            mTimeElapsed -= 1.0f;
            mTimerSec -= 1;

            // If the seconds goes negative then rollover the minutes
            if (mTimerSec <= 0 && !mGameDone)
            {
                if (mTimerMin == 0)
                {
                    mGameDone = true;
                    BroadcastMessage("GameOver");
                }
                else
                {
                    mTimerMin -= 1;
                    mTimerSec = 59;
                }
            }

            if (mGameDone)
            {
                timerText.text = "Game Over";
                if (mTimerSec + sceneTransitionTime <= 0)
                {
                    SceneManager.LoadScene("Scoreboard");
                }
            }
            else
            {
                timerText.text = mTimerMin.ToString() + ":" + mTimerSec.ToString("00");
            }
        }
    }

    // --------------------------------------------------
    // Returns the value of the mGameDone variable
    // --------------------------------------------------
    public bool GameDone()
    {
        return mGameDone;
    }

}
