﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public static TextBoxManager instance;

	[HideInInspector]
	public string[] textLines;
	[HideInInspector]
	public string speakerName;
	[HideInInspector]
	public int currentLine;
	[HideInInspector]
	public int endAtLine;
	public bool isActive;
	public bool stopPlayerMovement;
	public float typeSpeed;
	public string textScrollSFX;
	public bool viewingText = false;

	//public TextAsset textfile;
	[HideInInspector]
	public PlayerController player;
	public GameObject textBox;
	[HideInInspector]
	public Text theText;
	public GameObject nameBox;
	[HideInInspector]
	public Text nameText;
	public GameObject dialogueBG;
	public GameObject portrait;
	public GameObject portrait2;

	bool isNamed;
	bool isTyping = false;
	bool cancelTyping = false;



	NPCController character;
	
	//Events
	public delegate void OpenTextEvent();
	public static event OpenTextEvent openTextEvent;
	public delegate void CloseTextEvent();
	public static event CloseTextEvent closeTextEvent;


	void Awake(){
		if(instance == null)
			instance = this;
		else
			Destroy(gameObject);
		
	}

	void OnEnable(){
		//openTextEvent += OpenNameBox();
		//closeTextEvent += CloseNameBox();

	}

	void OnDisable(){

	}
	

	void Start(){
		GetComponents();

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
		dialogueBG.SetActive(true);
		textBox.SetActive(true);
		if(speakerName != ""){
			OpenNameBox();
		}		
		isActive = true;
		if(stopPlayerMovement) 
			player.canMove = false;

		StartCoroutine("TextScroll", textLines[currentLine]);
			
		//openTextEvent();
	}

	public void DisableTextBox(){
		dialogueBG.SetActive(false);		
		textBox.SetActive(false);
		if(nameBox.activeSelf)
			CloseNameBox();
		if(portrait.activeSelf || portrait2.activeSelf)
			ClosePortrait();
			
		isActive = false;
		player.canMove = true;
		
		//closeTextEvent();
	}

	public void OpenNameBox(){
		nameText.text = speakerName;
		nameBox.SetActive(true);
		if(portrait2.GetComponent<Image>().sprite != null)
			portrait2.SetActive(true);
	}

	public void CloseNameBox(){
		nameText.text = "";		
		nameBox.SetActive(false);
	}

	public void OpenPortrait(){
		portrait.SetActive(true);
		if(portrait2 != null)
			portrait2.SetActive(true);
	}

	public void ClosePortrait(){
		portrait.SetActive(false);
		portrait2.SetActive(false);
	}

	public void LoadScript(TextAsset theText){
		if(theText != null){
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));
			
		}
	}

	void GetComponents(){
		player = FindObjectOfType<PlayerController>();	
		theText = textBox.GetComponentInChildren<Text>();
		nameText = nameBox.GetComponentInChildren<Text>();
		portrait = GameObject.Find("Portrait");
		portrait2 = GameObject.Find("Portrait2");
		dialogueBG = GameObject.Find("DialogueBackground");
	}

	void IterateThroughTextBox(){
		//theText.text = textLines[currentLine];
		if(Input.GetKeyDown(KeyCode.Return)){
			if(!isTyping){	
				currentLine++;

				if(currentLine <= endAtLine)
					GetName(textLines);

				if(currentLine > endAtLine){
					DisableTextBox();
					currentLine = 0;
				}
				else{
					StartCoroutine("TextScroll", textLines[currentLine]);
				}
			}
			else if(isTyping && !cancelTyping){
				cancelTyping = true;
			}
		}
	}

	public IEnumerator TextScroll(string lineOfText){
		int letter = 0;
		bool everyOtherLoop = true;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while(isTyping && !cancelTyping && (letter < lineOfText.Length - 1)){
			theText.text += lineOfText[letter];
			letter++;
			everyOtherLoop = !everyOtherLoop;
			if (!everyOtherLoop) 
				AudioManager.PlayVariedEffect(textScrollSFX);

			yield return new WaitForSeconds(typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}

	public void GetName(string[] line){
		if(line[currentLine].Contains("name[")){
			int i = line[currentLine].IndexOf("[");
			int j = line[currentLine].IndexOf("]", i);
			string tempString = line[currentLine].Substring(i - 4, j - i + 1 + 4 + 1);
			speakerName = line[currentLine].Substring(i + 1, j - i - 1);
			nameText.text = speakerName;
			line[currentLine] = line[currentLine].Replace(tempString, "");
			OpenNameBox();
		}
		else{
			speakerName = "";
			nameText.text = speakerName;
			CloseNameBox();
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