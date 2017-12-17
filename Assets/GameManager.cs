using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject inputfield;
	public GameObject endGameText;
	public string culpritName;

	bool isCorrect;

	void Start(){
		inputfield = GameObject.Find("InputField");
		endGameText = GameObject.Find("EndGameText");
		inputfield.SetActive(false);
		endGameText.SetActive(false);
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

		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public void GetInput(string guess){
		Debug.Log("GotInput: " + guess);
		if(guess.ToLower() == culpritName.ToLower()){
			Debug.Log("You win!");
			isCorrect = true;
		}
		else{
			Debug.Log("You Lose!");
			isCorrect = false;
		}

		StartCoroutine("EndGameCoroutine", isCorrect);
	}

	IEnumerator EndGameCoroutine(bool winCondition){
		if(winCondition){
			endGameText.GetComponent<Text>().text = "You Win!";
			endGameText.SetActive(true);
		}
		else{
			endGameText.GetComponent<Text>().text = "Wrong! Game Over!";
			endGameText.SetActive(true);
		}

		yield return new WaitForSeconds(5.0f);
		SceneManager.LoadScene("scratch");

	}
}
