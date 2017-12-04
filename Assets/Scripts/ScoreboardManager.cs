using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScoreboardManager : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public Text ScoreboardText;

    // ------------------------------
    // Private variables
    // ------------------------------
    AudioSource mGuiClickSound;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        // Get the GUI click sound
        mGuiClickSound = GameObject.Find("GuiClickSound").GetComponent<AudioSource>();

        // Get the time limit and score from the last game
        int gameLengthSec = PlayerPrefs.GetInt("TimeLimit");
        int gameScore = PlayerPrefs.GetInt("Score");

        // Display to the scoreboard
        string timeMinStr = (gameLengthSec / 60).ToString();
        string timeSecStr = (gameLengthSec % 60).ToString("00");
        ScoreboardText.text = "Time Limit - " + timeMinStr + ":" + timeSecStr + "\nScore - " + gameScore.ToString();
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {
        
    }

    // --------------------------------------------------
    // Reload the main game when the try again button is clicked
    // --------------------------------------------------
    public void TryAgainClick ()
    {
        mGuiClickSound.Play();

        SceneManager.LoadScene("Main");
    }

    // --------------------------------------------------
    // Go back to the main menu when the main menu button is clicked
    // --------------------------------------------------
    public void MainMenuClick()
    {
        mGuiClickSound.Play();

        SceneManager.LoadScene("Menu");
    }
}
