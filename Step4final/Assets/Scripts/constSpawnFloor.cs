﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class constSpawnFloor : MonoBehaviour {

    BoxCollider2D collider;

    private float colliderWidth;

    public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogError("Can't find the floor - might be lava!");
        }
        colliderWidth = collider.size.x;
        Debug.Log(colliderWidth);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -17.8f)
        {
            transform.position = new Vector3(spawnPoint.transform.position.x + (colliderWidth/2), 
                                            spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        }
	}
}
