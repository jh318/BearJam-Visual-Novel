using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if(waitForPress && Input.GetKeyDown(KeyCode.J)){
			StartText();
		}
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
		if(c.GetComponent<PlayerController>()){
			waitForPress = false;
		}
	}

	void StartText(){
		theTextBox.ReloadScript(theText);
		theTextBox.currentLine = startLine;
		theTextBox.endAtLine = endLine;
		theTextBox.EnableTextBox();
		if(character != null){
			TextBoxManager.instance.nameText.text = character.characterName;			
			TextBoxManager.instance.OpenNameBox();
		}
			

		if(destroyWhenActivated){
			Destroy(gameObject);
		}
	}
}
