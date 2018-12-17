using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Data;

/* Script called from controller when isGameOver == true */
public class UpdateScoreboard : MonoBehaviour
{
    /* Needs the game controller to access score and name */
    GameController myGameController;

    /* Screen name of player */
    /* Will be inherited from GameController in final version */
    string name;

    /* Score of game that just ended (converted to string)*/
    /* Will be inherited from GameController in final version */
    public float newHighScore;

    /* Current high score */
    int currentHighScore;

    /* mySQL login info */
    string connectionString = "SERVER=step4.c22vwamy1vuf.us-east-2.rds.amazonaws.com;" + "DATABASE=step4;" + "UID=step4;" + "PASSWORD=SoftwareDev123;";

    /* Called in Start() - completes query to check user's current high score in mySql databbase */
    public void CheckScore()
    {
        /* Declare new mySQL connection and command */
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        using (MySqlCommand command = new MySqlCommand())
        {
            /* set up SQL query */
            string query = "SELECT score FROM highscores WHERE displayName = '" + name + "';";
            string commandText = string.Format(query);
            command.CommandText = commandText;
            command.Connection = connection;
            command.Connection.Open();
            /* Process query through game */
            try
            {
                MySql.Data.MySqlClient.MySqlDataReader scores;
                using (scores = command.ExecuteReader())
                {
                    /* Storing query results */
                    while (scores.Read())
                    {
                        currentHighScore = Convert.ToInt32(scores["score"]);
                    }
                    /* Checking if score is a new high score */
                    if (currentHighScore < newHighScore)
                    {
                        /* Calls upload function if true */
                        UploadScore();
                    }
                }

            }
            /* If for some reason the query doesn't work or the connection fails */
            catch (System.Exception ex)
            {
                Debug.LogError("MySQL error: " + ex.ToString());
            }
        }
    }

    /* Called from CheckScore() when it's determined a new highscore has been set */
    public void UploadScore()
    {
        /* Declare new mySQL connection and command */
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        using (MySqlCommand command = new MySqlCommand())
        {
            /* set up SQL query */
            string nonQuery = "UPDATE highscores SET score = " + newHighScore + " WHERE displayName = '" + name + "';";
            string commandText = string.Format(nonQuery);
            command.CommandText = commandText;
            command.Connection = connection;
            command.Connection.Open();
            /* Attempts to push score to database */
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("MySQL error: " + ex.ToString());
            }

        }
    }
    // Use this for initialization
    void Start()
    {
        // Game Object is also set to false by default in editor
        gameObject.SetActive(false);
        myGameController = GameObject.FindObjectOfType<GameController>();
    }

    void OnEnable()
    {
        newHighScore = GameController.score;
        name = GameController.screenName;
        // Retrieves players score and name from controller
        Debug.Log("scoreboard script successfully called!");
        // Checks score and updates if necessary
        CheckScore();
        // Deactivated game object upon completion
        gameObject.SetActive(false);
    }
}