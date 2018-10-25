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
        powerup.value = 0;
        powerUpTime = 10.0;
        timeAMT = powerUpTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        while(powerup.value < 1.0)
        {
            if (PowerUp = PowerUp + 1.0)
            {
                powerup.value = powerup.value + .01;
            }
        }
        if(powerup.value == 1.0)
        {
            isPowerUpAvail = true;
            powerUpTime -= Time.deltaTime;
            powerup.value = timeAMT / powerUpTime;

        }
        Start();
	}
}
