using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject inputfield;
	public string culpritName;

	void Start(){
		inputfield = GameObject.Find("InputField");
		inputfield.SetActive(false);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)){
			Debug.Log("toggle input");
			if(!inputfield.activeSelf)
				inputfield.SetActive(true);
			else
				inputfield.SetActive(false);
		}

		if(inputfield.activeSelf)
			PlayerController.instance.canMove = false;
		else
			PlayerController.instance.canMove = true;
	}

	public void GetInput(string guess){
		Debug.Log("GotInput: " + guess);
		if(guess.ToLower() == culpritName.ToLower()){
			Debug.Log("You win!");
		}
		else{
			Debug.Log("You Lose!");
		}
	}
}
