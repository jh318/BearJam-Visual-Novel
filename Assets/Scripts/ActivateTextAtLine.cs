using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateTextAtLine : MonoBehaviour {


	public int startLine;
	public int endLine;
	public bool requireButtonPress;
	public bool destroyWhenActivated;

	private bool waitForPress;

	[HideInInspector]
	public TextBoxManager theTextBox;
	public TextAsset theText;

	NPCController character;

	void Start(){
		theTextBox = FindObjectOfType<TextBoxManager>();
		if(GetComponent<NPCController>())
			character = GetComponent<NPCController>();
		else if(GetComponentInChildren<NPCController>())
			character = GetComponentInChildren<NPCController>();
	}

	void Update(){
		if(waitForPress && Input.GetKeyDown(KeyCode.J))
			StartText();
	}

	
	void OnTriggerEnter2D(Collider2D c){
		if(c.GetComponent<PlayerController>()){
			if(requireButtonPress){
				waitForPress = true;
				return;
			}
			StartText();
		}
	}

	void OnTriggerExit2D(Collider2D c){
		if(c.GetComponent<PlayerController>())
			waitForPress = false;
	}

	void StartText(){
		theTextBox.LoadScript(theText);
		theTextBox.currentLine = startLine;
		theTextBox.endAtLine = endLine;
		TextBoxManager.instance.GetName(TextBoxManager.instance.textLines);	
		theTextBox.EnableTextBox();
	
		if(character.characterPortrait != null){
			TextBoxManager.instance.portrait.GetComponent<Image>().sprite = character.characterPortrait; 
			TextBoxManager.instance.OpenPortrait();
		}					

		if(destroyWhenActivated)
			Destroy(gameObject);	
	}
}
