using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public static TextBoxManager instance;

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
	public GameObject textBox;
	[HideInInspector]
	public Text theText;
	public GameObject nameBox;
	[HideInInspector]
	public Text nameText;

	bool isNamed;

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
		player = FindObjectOfType<PlayerController>();	
		theText = textBox.GetComponentInChildren<Text>();
		nameText = nameBox.GetComponentInChildren<Text>();
		// if(GetComponentInChildren<NPCController>())
		// 	character = GetComponentInChildren<NPCController>();

		
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
		
		
//		openTextEvent();
	}

	public void DisableTextBox(){
		textBox.SetActive(false);
		if(nameBox.activeSelf)
			CloseNameBox();
		isActive = false;
		player.canMove = true;
		
	//	closeTextEvent();
	}

	public void OpenNameBox(){
		nameBox.SetActive(true);
	}

	public void CloseNameBox(){
		nameBox.SetActive(false);
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