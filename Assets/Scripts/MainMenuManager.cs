using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{

    // ------------------------------
    // Public variables
    // ------------------------------
    public Text howToPlayText;
    public Text gameTitleText;
    public Text gameSubTitleText;
    public Text timeLimitText;
    public Button howToPlayButton;
    public Button newGameButton;
    public Button backButton;
    public Button startButton;
    public Button quitButton;
    public Dropdown timeDropdown;

    // ------------------------------
    // Private variables
    // ------------------------------
    int mGameLengthSec = 30;
    AudioSource mGuiClickSound;

    // -----------------------------------------------
    // VirtualFunction: Use this for initialization
    // -----------------------------------------------
    void Start()
    {
        // Get the GUI click sound
        mGuiClickSound = GameObject.Find("GuiClickSound").GetComponent<AudioSource>();

        // Disable the back button
        backButton.enabled = false;
        backButton.GetComponentInChildren<Image>().enabled = false;
        backButton.GetComponentInChildren<Text>().enabled = false;

        // Disable the start button
        startButton.enabled = false;
        startButton.GetComponentInChildren<Image>().enabled = false;
        startButton.GetComponentInChildren<Text>().enabled = false;

        // Disable the dropdown menu
        Image[] imgs = timeDropdown.GetComponentsInChildren<Image>();
        imgs[0].enabled = false;
        imgs[1].enabled = false;
        timeDropdown.enabled = false;
        timeDropdown.GetComponentInChildren<Dropdown>().enabled = false;
        timeDropdown.GetComponentInChildren<Text>().enabled = false;
    }

    // --------------------------------------------------
    // VirtualFunction: Update is called once per frame
    // --------------------------------------------------
    void Update()
    {

    }

    // --------------------------------------------------
    // Show the new game options when the new game button is pressed
    // --------------------------------------------------
    public void NewGameClick()
    {
        mGuiClickSound.Play();

        howToPlayText.enabled = false;
        gameTitleText.enabled = true;
        gameSubTitleText.enabled = true;
        timeLimitText.enabled = true;

        newGameButton.enabled = false;
        newGameButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        newGameButton.GetComponentInChildren<Text>().color = Color.clear;

        howToPlayButton.enabled = false;
        howToPlayButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        howToPlayButton.GetComponentInChildren<Text>().color = Color.clear;

        backButton.enabled = false;
        backButton.GetComponentInChildren<Image>().enabled = false;
        backButton.GetComponentInChildren<Text>().enabled = false;

        quitButton.enabled = false;
        quitButton.GetComponentInChildren<Image>().enabled = false;
        quitButton.GetComponentInChildren<Text>().enabled = false;

        // Enable the start button
        startButton.enabled = true;
        startButton.GetComponentInChildren<Image>().enabled = true;
        startButton.GetComponentInChildren<Text>().enabled = true;

        // Enable the dropdown menu
        timeDropdown.enabled = true;
        Image[] imgs = timeDropdown.GetComponentsInChildren<Image>();
        imgs[0].enabled = true;
        imgs[1].enabled = true;
        timeDropdown.GetComponentInChildren<Dropdown>().enabled = true;
        timeDropdown.GetComponentInChildren<Text>().enabled = true;        
    }

    // --------------------------------------------------
    // Show the help text when the how to play button is pressed
    // --------------------------------------------------
    public void HowToPlayClick()
    {
        mGuiClickSound.Play();

        howToPlayText.enabled = true;
        gameTitleText.enabled = false;
        gameSubTitleText.enabled = false;
        
        newGameButton.enabled = false;
        newGameButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        newGameButton.GetComponentInChildren<Text>().color = Color.clear;

        quitButton.enabled = false;
        quitButton.GetComponentInChildren<Image>().enabled = false;
        quitButton.GetComponentInChildren<Text>().enabled = false;

        howToPlayButton.enabled = false;
        howToPlayButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        howToPlayButton.GetComponentInChildren<Text>().color = Color.clear;

        backButton.enabled = true;
        backButton.GetComponentInChildren<Image>().enabled = true;
        backButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
        backButton.GetComponentInChildren<Text>().enabled = true;
        backButton.GetComponentInChildren<Text>().color = Color.black;
    }

    // --------------------------------------------------
    // Return to the main menu when back button is clicked
    // --------------------------------------------------
    public void BackClick()
    {
        mGuiClickSound.Play();

        howToPlayText.enabled = false;
        gameTitleText.enabled = true;
        gameSubTitleText.enabled = true;

        newGameButton.enabled = true;
        newGameButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
        newGameButton.GetComponentInChildren<Text>().color = Color.black;

        howToPlayButton.enabled = true;
        howToPlayButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
        howToPlayButton.GetComponentInChildren<Text>().color = Color.black;

        quitButton.enabled = true;
        quitButton.GetComponentInChildren<Image>().enabled = true;
        quitButton.GetComponentInChildren<Text>().enabled = true;

        backButton.enabled = false;
        backButton.GetComponentInChildren<Image>().enabled = false;
        backButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
        backButton.GetComponentInChildren<Text>().enabled = false;
        backButton.GetComponentInChildren<Text>().color = Color.clear;
    }

    // --------------------------------------------------
    // Exit the game when the Quit button is pressed
    // --------------------------------------------------
    public void QuitClick()
    {
        mGuiClickSound.Play();

        Application.Quit();
    }

    // --------------------------------------------------
    // Update the game length when the dropdown menu changes
    // --------------------------------------------------
    public void DropDownUpdate()
    {
        string gameLengthStr = timeDropdown.GetComponentInChildren<Text>().text;
        string resultString = System.Text.RegularExpressions.Regex.Match(gameLengthStr, @"\d+").Value;

        //Debug.Log(System.Int32.Parse(resultString).ToString());
        mGameLengthSec = System.Int32.Parse(resultString);
        //mGameLengthSec = 30;
    }

    // --------------------------------------------------
    // Start the game using the specified time limit
    // --------------------------------------------------
    public void StartClick()
    {
        mGuiClickSound.Play();

        PlayerPrefs.SetInt("TimeLimit", mGameLengthSec);
        SceneManager.LoadScene("Main");
    }
}
