using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	[HideInInspector]
	public string[] textLines;
	[HideInInspector]
	public int currentLine;
	[HideInInspector]
	public int endAtLine;
	public bool isActive;
	public bool stopPlayerMovement;

	[HideInInspector]
	public PlayerController player;
	//public TextAsset textfile;
	public Text theText;
	public GameObject textBox;

	

	void Start(){
		player = FindObjectOfType<PlayerController>();
		
		//GetTextFile();
		
		if(isActive){
			EnableTextBox();
		}
		else{
			DisableTextBox();
		}
	}

	void Update(){
		if(!isActive){
			return; 
		}

		IterateThroughTextBox();
	}

	public void EnableTextBox(){
		textBox.SetActive(true);
		isActive = true;
		if(stopPlayerMovement) 
			player.canMove = false;
		
	}

	public void DisableTextBox(){
		textBox.SetActive(false);
		isActive = false;
		player.canMove = true;
	}

	public void ReloadScript(TextAsset theText){
		if(theText != null){
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));
		}
	}

	void IterateThroughTextBox(){
		theText.text = textLines[currentLine];

		if(Input.GetKeyDown(KeyCode.Return)){
			currentLine++;
		}

		if(currentLine > endAtLine){
			DisableTextBox();
			currentLine = 0;
		}
	}

	// void GetTextFile(){
	// 	if(textfile != null){
	// 		textLines = textfile.text.Split('\n');
	// 	}

	// 	if(endAtLine == 0){
	// 		endAtLine = textLines.Length - 1;
	// 	}
	// }

}