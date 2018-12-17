﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+-------------------- THESE DEAL WITH SCORING THE PLAYER AND DISPLAYING DATA VIA SESSION UIs------------------------+*/
/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
    
    // These are used to hold and display the score of the the current game session
    public Text ScoreText;
    public static float score = 0.0f;

    // These are used to hold and display the power of the current game session
    public Text PowerText;
    public float power = 0.0f;

    public static bool powerUpActive = false;

    // This hold the desired number of point to be added to the player's score every
    // second of gameplay
    public float scorePerSecond;

    // These variables hold the player's current high score, and the score of the newly finished game
    // as strings. These are used in the information section of the Game Over Screen.
    //+++++++ NOTE: THESE MAY NEED TO HAVE REFERENCES REATTACHED TO DO THIS COMMENT OUT THE HIDE IN INPSECTOR!!!! +++++
    [HideInInspector]
    public Text highScoreText;
    [HideInInspector]
    public Text gameScoreText;

    // This object is used to display the New_Tag in the Game Over screen.
    public GameObject newTag;

    // This object is used to display the game over screen
    public GameObject gameOverScreen;

    // This object will = object containing the script
    public GameObject mySqlObject;

    // CREATE player name that is somehow obtained from user's login
    public static string screenName;

    /* -> FUNCTIONS <- */

        // public void incrementScore()
            // This function is used to increment the score of the player during the given game sesssion
            // It is included as a function for alteration in how a player is scored and/or what each
            // scoring activity yeilds the player

        // public void incrementPower()
            // This function is used to increment the number of power coins that the 
            // player has collected in the current game sesison.

        // void ShowOverPanel()
            // This function causes the Game Over screen to appear once the game session has terminated. 
            // This will also handle displaying the information of the player's current high score, previous
            // game score, and if the New_Tag should be displayed.




/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+----------------------- THESE DEAL WITH GAME OBSTACLE GENERATION AND FUNCTION -------------------------------------+*/
/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

    //This controls the speed at which the obstacles will move toward the player
    public float scrollSpeed = 12.0f;

    // This stores all of the available obstacles in an array of GameObjects
    public GameObject[] Obstacles;


    // This variable controlls how many obstacles that the game generates per second
    public float obsFreq;

    // Cunter variable that is used to determine how much time has passed
    // This is used to determine when a new obstacle should be generated
    public float counter;

    // This determines the posititon where the obstacles will spawn
    public Transform obstacleSpawnPos;

    /* -> FUNCTIONS <- */

        // void scrollObstacle(GameObject currentObstacle, float speed)
            // This allows the current obstacle to scroll towards the player from the right side of 
            // the screen to the left side of the screen

        // void generateRandomObstacle()
            // This function is responsible for generating the obstacles; and resetting the counter
            // so that the game will contine to generate obstacles

/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+----------------------- THESE DEAL WITH GENERAL GAME FUNCTIONALITY ------------------------------------------------+*/
/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

    // This bool is set to true whenever the condidtons for a game over are met
    public bool isGameOver = false;

    // This is the background music for the game
    private AudioSource bgMusic;

    /* -> FUNCTIONS <- */

    // public void GameOver()
    // This function is called whenever the conditions for a game over are met. It
    // sets isGameOver to true.


/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*--------------------------------- AUDIO CLIP REFERENCES --------------------------------------------------------------*/
/*++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

        public AudioSource blowup;
    

/*======================================================================================================================*/
/*======================================================================================================================*/
/*|||                                               GAME CODE                                                        |||*/
/*======================================================================================================================*/
/*======================================================================================================================*/

    // Use this for initialization
    void Start() {
        bgMusic = GetComponent<AudioSource>();
        if (bgMusic == null)
        {
            Debug.LogError("Can't find background music!");
        }
        // Find object with scoreboard script
        mySqlObject = gameObject.transform.Find("scoreboardScript").gameObject;
        // Start generating obstacles
        generateRandomObstacle();
    }

    // Update is called once per frame
    void Update() {
        if (isGameOver)
        {
            return;
        }

        // Constantly increment the players score
        incrementScore();

        // This generates the obstacles that the player must traverse with the specified
        // frequency, obsFreq.
        if (counter < 0.0f)
        {
            if (powerUpActive)
            {
                decrementPower();
            }
            generateRandomObstacle();
        }
        else
        {
            counter -= Time.deltaTime * obsFreq;
        }

        // Make a game object called currentChild (called this because it is a child of the
        // obstacle controller.)
        GameObject currentChild;

        // This is responsible for the game continuously scrolling
            for (int i = 0; i < transform.childCount; i++)
            {
                // Instantiate the currentChild (the current obstacle)
                // and make it scroll toward the player
                currentChild = transform.GetChild(i).gameObject;
                scrollObstacle(currentChild, scrollSpeed);

                // Destroy the obstacle once it has left the screen
                if (currentChild.transform.position.x <= -150.0f)
                {
                    Destroy(currentChild);
                }
            }
    }

/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/
/*+--------------------------------------------- FUNCTION DEFINITIONS ------------------------------------------------+*/
/*+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++*/

    public void scrollObstacle(GameObject currentObstacle, float speed)
    {
        currentObstacle.transform.position -= Vector3.right * (speed * Time.deltaTime);
    }

    
    void generateRandomObstacle()
    {
        GameObject newObs = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], obstacleSpawnPos.position, Quaternion.identity) as GameObject;
        newObs.transform.parent = transform;

        // NOTE: Changing this value will chang the frequncy of challenge/obstacle generation.
        counter = 5.0f;
    }

    
    public void GameOver()
    {
        isGameOver = true;
        bgMusic.enabled = false;
        blowup.Play();
        Invoke("ShowOverPanel", 1.0f);
        // Runs scoreboard check and update - script disables itself upon completion
        mySqlObject.SetActive(true);
    }


    void ShowOverPanel()
    {
        // Disable the on-screen score counter
        ScoreText.gameObject.SetActive(false);

        // Disable the on-screen power counter
        PowerText.gameObject.SetActive(false);


        // If the player has hit a new high score
        if (score > PlayerPrefs.GetInt("High_Score", 0))
        {
            newTag.SetActive(true);
            PlayerPrefs.SetInt("High_Score", Mathf.RoundToInt(score));
        }

        // Set the text field of highScoreText, the highest score in the Game Over UI, to contain the 
        //proper text display and the user's current high score.
        // highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High_Score", 0).ToString();
        
        // Set the text field of gameScoreText, the current game score in the Game Over UI, to contain
        // the proper text display and the score from the game the user just played.
        gameScoreText.text = "Score: " + Mathf.Round(score);

        // Display the Game Over Screen
        gameOverScreen.SetActive(true);
    }


    public void Restart ()
    {
        isGameOver = false;
        Application.LoadLevel(Application.loadedLevelName);
    }

    
    public void incrementScore()
    {
        score += scorePerSecond * Time.deltaTime;
        ScoreText.text = "Score: " + Mathf.Round(score);
    }

    
    public void incrementPower()
    {
        power++;
        PowerText.text = "Power: " + power;
    } 

    public void decrementPower()
    {
        power--;
        PowerText.text = "Power: " + power;
    }
}
