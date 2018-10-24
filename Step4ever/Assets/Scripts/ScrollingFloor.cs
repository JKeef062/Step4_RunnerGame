using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingFloor : MonoBehaviour {

    Rigidbody2D floor;

    GameController controller;

	// Use this for initialization
	void Start () {
        floor = GetComponent<Rigidbody2D>();
        controller = GameObject.FindObjectOfType<GameController>();
        floor.velocity = new Vector2(-controller.scrollSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if(controller.isGameOver)
        {
            floor.velocity = Vector2.zero;
        }
		
	}
}
