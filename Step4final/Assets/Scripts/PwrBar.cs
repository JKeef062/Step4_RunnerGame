using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PwrBar : MonoBehaviour {

    // We want the power up bar to fill in increments of 20%. Therefore a full power up bar is 5 pumpkin collections
    Image fillBar;
    float fullPumps = 5.0f;
    GameController myGameController;
    public Text pwrUpAvail;

	// Use this for initialization
	void Start () {
        // Get a reference to the bar image
        fillBar = GetComponent<Image>();

        // GameController needed to update the number of power coins
        myGameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        fillBar.fillAmount = myGameController.power / fullPumps;
        
        // Show power up available text if bar is full
        if (fillBar.fillAmount >= 1)
        {
            pwrUpAvail.gameObject.SetActive(true);
            GameController.powerUpActive = true;
        }
        if (fillBar.fillAmount == 0)
        {
            pwrUpAvail.gameObject.SetActive(false);
            GameController.powerUpActive = false;
        }

    }
}
