using UnityEngine;

public class LevelStartScroll : MonoBehaviour {

    public float scrollSpeed;

    public GameObject floor;

    public Transform floorSpawn;

    // Scroll the floor and destroy it once the player is interacting with challenge obstacles
	void Update () {
        floor.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);

        if(floor.transform.position.x < -57.0f)
        {
            //GameObject newObs = Instantiate(floor, floorSpawn.position, Quaternion.identity) as GameObject;
            Destroy(floor);
        }

        return;
	}
}
