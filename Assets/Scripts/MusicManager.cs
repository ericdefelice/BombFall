using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    // ------------------------------
    // Public variables
    // ------------------------------
    public GameObject musicPlayer;

    // ------------------------------
    // Private variables
    // ------------------------------

    // -----------------------------------------------
    // VirtualFunction: Called on object initializiation
    // -----------------------------------------------
    void Awake()
    {
        //When the scene loads it checks if there is an object called "Music".
        musicPlayer = GameObject.Find("Music");
        if (musicPlayer == null)
        {
            //If this object does not exist, then use the one this script is attached to
            musicPlayer = this.gameObject;
            musicPlayer.name = "Music";

            // Don't unload the music when the scene changes
            DontDestroyOnLoad(musicPlayer);
        }
        else
        {
            // If these scene does have a Music object already, then delete this one
            if (this.gameObject.name != "Music")
            {
                Destroy(this.gameObject);
            }
        }

        if (!musicPlayer.GetComponent<AudioSource>().isPlaying)
            musicPlayer.GetComponent<AudioSource>().Play();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


