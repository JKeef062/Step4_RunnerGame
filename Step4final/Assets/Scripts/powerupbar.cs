/*
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerupbar : MonoBehaviour {

    public GameController controller;
    public bool isPowerUpAvail;
    public float powerUpTime;
    public float timeAMT;

    public Slider powerup;
    public float PowerUp = controller.power;
    

	// Use this for initialization
	void Start () {
        powerup.value = 0f;
        powerUpTime = 10.0f;
        timeAMT = powerUpTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        while(powerup.value < 1.0f)
        {
            if (PowerUp = PowerUp + 1.0f)
            {
                powerup.value = powerup.value + .01f;
            }
        }
        if(powerup.value == 1.0f)
        {
            isPowerUpAvail = true;
            powerUpTime -= Time.deltaTime;
            powerup.value = timeAMT / powerUpTime;

        }
        Start();
	}
}
*/