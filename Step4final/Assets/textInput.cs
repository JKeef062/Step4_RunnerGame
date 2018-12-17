using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textInput : MonoBehaviour {

    public InputField mainInputField;


	// Use this for initialization
	void Start () {
        mainInputField = transform.GetComponent<InputField>();
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
