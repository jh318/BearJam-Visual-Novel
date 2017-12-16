using System.Collections;
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


	[HideInInspector]
	public PlayerController player;
	//public TextAsset textfile;
	public GameObject textBox;
	[HideInInspector]
	public Text theText;
	public GameObject nameBox;
	[HideInInspector]
	public Text nameText;
	public GameObject portrait;

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
		textBox.SetActive(true);
		if(speakerName != ""){
			OpenNameBox();
		}
		isActive = true;
		if(stopPlayerMovement) 
			player.canMove = false;
			
		//openTextEvent();
	}

	public void DisableTextBox(){
		textBox.SetActive(false);
		if(nameBox.activeSelf)
			CloseNameBox();
		if(portrait.activeSelf)
			ClosePortrait();
		isActive = false;
		player.canMove = true;
		
		//closeTextEvent();
	}

	public void OpenNameBox(){
		nameText.text = speakerName;
		nameBox.SetActive(true);
	}

	public void CloseNameBox(){
		nameText.text = "";		
		nameBox.SetActive(false);
	}

	public void OpenPortrait(){
		portrait.SetActive(true);
	}

	public void ClosePortrait(){
		portrait.SetActive(false);
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
	}

	void IterateThroughTextBox(){
		theText.text = textLines[currentLine];

		if(Input.GetKeyDown(KeyCode.Return)){
			currentLine++;
			GetName(textLines);

		}

		if(currentLine > endAtLine){
			DisableTextBox();
			currentLine = 0;
		}
	}

	public void GetName(string[] line){
		if(line[currentLine].Contains("name[")){
			Debug.Log("hasname");
			int i = line[currentLine].IndexOf("[");
			int j = line[currentLine].IndexOf("]", i);
			string tempString = line[currentLine].Substring(i - 4, j - i + 1 + 4 + 1);
			Debug.Log(tempString);
			speakerName = line[currentLine].Substring(i + 1, j - i - 1);
			nameText.text = speakerName;
			Debug.Log(speakerName);
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