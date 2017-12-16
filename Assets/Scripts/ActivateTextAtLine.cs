using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextBoxManager theTextBox;

	public bool requireButtonPress;
	private bool waitForPress;

	public bool destroyWhenActivated;

	void Start(){
		theTextBox = FindObjectOfType<TextBoxManager>();

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

		if(destroyWhenActivated){
			Destroy(gameObject);
		}
	}
}
