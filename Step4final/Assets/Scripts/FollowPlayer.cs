using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    // Reference to the player
    public Transform player;

    // This is the block used to determine if the camera should follow the player or not
    public Transform followBounds;

    // This is used to establish the desired camera view of the game
    public Vector3 cameraView;

    // This holds the base view of the game so that it can return to it when player is on the
    // lowest altitude platform.
    private Vector3 baseView;

    // These is used to control the camera lag when following the player
    public float graduality;
    public float highAltGraduality;

    // This is to access game over flag
    public GameController theController;

    void Start()
    {
        baseView = player.position + cameraView;
        // This sets the initial view of the camera to the desired location in relation to the player
        transform.position = baseView;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if(theController.isGameOver)
        {
            return;
        }

        if (player.position.y > followBounds.position.y)
        {

            Vector3 targetPosition = player.position + cameraView;
            Vector3 followPosition = Vector3.Lerp(transform.position, targetPosition, highAltGraduality);
            transform.position = followPosition;
        }
        else
        {
            Vector3 followPosition = Vector3.Lerp(transform.position, baseView, graduality);
            transform.position = followPosition;
        }
	}
}
