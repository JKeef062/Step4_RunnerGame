using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    // This, along with gravity scale in the rigid bod component, will determine how high
    // the player will be allowed to jump
    public float jumpPower = 10.0f;

    // Instantiates a Rigidbody2D object used in this script to refer to the player in the game engine
    Rigidbody2D Player;

    // This value is true whenever the player is in contact with an object that contains the tag "Ground"
    // This is false whenever the player is in the air. When false, the player will be unable to perform
    // a jump action
    public bool onGround = false;

    public static bool powerUpActive = true;
    
    // This variable is used to determine if the position of the player has changed along the x-axis of the 
    // screen. This will occur whenever the player is hit by an incoming obstacle.
    float xPos = 0.0f;

    // This value is used to signal when a game session should terminate. Will be set to true whenever
    // the conditions for a game over are met
    bool isGameOver = false;

    // Creates an object of GameController that will allow this script to utilize any public functionality
    // from the script GameControlller.
    GameController myGameConroller;

    // This is the ectoplasm death particles for the player
    public GameObject ecto;

    // This is the player death sound
    private AudioSource powerUP;

    // These variable will determine for how long the player can jump after the inital jump leaving the groune
    // Essentially this is how long the held jump button will register
    public float heldJumptime;
    private float highJumpCounter;
    public bool isJumping;

	// Use this for initialization
	void Start () {
        // This instantiates a Rigidbody2D object that is the player. 
        Player = transform.GetComponent<Rigidbody2D>();
        if (Player == null)
        {
            Debug.LogError("Can't find the player! Honestly tho, he's a ghost like what did you expect?");
        }

        // This sets the value of xPos to the postion of the player on the screen.
        xPos = transform.position.x;

        // On the start of a game session allow access to functionality from the ObstacleController script
        myGameConroller = GameObject.FindObjectOfType<GameController>();
        if (myGameConroller == null)
        {
            Debug.LogError("We lost the controller!");
        }

        // This is the reference to the power up audio clip
        powerUP = GetComponent<AudioSource>();
        if (powerUP == null)
        {
            Debug.LogError("No powerup audio script");
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isGameOver)
        {
            return;
        }

		if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && (onGround || isJumping) && !isGameOver)
        {
            //isJumping = true;
            Player.AddForce(Vector3.up * (jumpPower * Player.mass * Player.gravityScale * 10.0f ));
            //Player.velocity = Vector2.up * jumpPower;
        }

        // Checks if the player's position along the x-axis of the screen has been changed. If it has
        // then the game is terminated.
        if (Player.position.x < xPos)
        {
            //Debug.Log("If Statement still active");
            KillDestPlayer();
            GameOver();
        }
	}

    // This function is used to set isGame over to true whenever conditions for a game over are met.
    // NOTE: SHOULD BE PLACED IN A GENERAL GAME FUNCTIONALITY SCRIPT HERE TO MAKE TEMPLATE
    void GameOver()
    {
        // This will show when a game over signal is called. This signal can be seen ocurring in
        // the Unity console
        isGameOver = true;
        Debug.Log("Game Over");
        myGameConroller.GameOver();
    }

    // This sets the value of onGround to true whenever the player is in contact with an object that has the
    // tag "Ground". Therefore, jumping is permitted when the player is on the ground
    private void OnCollisionEnter2D(Collision2D other)
    {
        powerUpActive = GameController.powerUpActive;
        if (other.collider.tag == "Ground")
        {
            onGround = true;
        }

        if (other.collider.tag == "Enemy"&&!powerUpActive)
        {
            KillDestPlayer();
            GameOver();
        }

        if (other.collider.tag == "Enemy" && powerUpActive)
        {
            onGround = true;
        }
    }

    // This maintains the value of onGround to true whenever the player is in contact with an object that
    // has the tag "Ground". Effectively this wil make sure that the player will always the option to jump
    // when they are on a ground surface
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            onGround = true;
        }
        if (other.collider.tag == "Enemy" && powerUpActive)
        {
            onGround = true;
        }
    }

    // This sets the value of on ground to false whenever the player becomes removed from a "Ground" tagged
    // surface. This makes it so that the player cannot jump while in thet air
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            onGround = false;
        }
        if (other.collider.tag == "Enemy" && powerUpActive)
        {
            onGround = false;
        }
    }

    // This function is called whenever the player collides with an object with the tag "Coin"
    // It will incrment the player's score then destroy the "Coin" object from the screen.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin" && !powerUpActive)
        {
            powerUP.Play();
            myGameConroller.incrementPower();
            Destroy(other.gameObject);
        }
        if (other.tag == "Coin" && powerUpActive)
        {
            Destroy(other.gameObject);
        }
    }

    private void KillDestPlayer()
    {
        Instantiate(ecto, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
