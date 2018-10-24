using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCastle : MonoBehaviour {

    Rigidbody2D castle;

    GameController controller;

	// Use this for initialization
	void Start () {
        castle = GetComponent<Rigidbody2D>();
        controller = GameObject.FindObjectOfType<GameController>();
        float castleSpeed = controller.scrollSpeed / 6;
        castle.velocity = new Vector2(-castleSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.isGameOver){
            castle.velocity = Vector2.zero;
        }
		
	}
}
