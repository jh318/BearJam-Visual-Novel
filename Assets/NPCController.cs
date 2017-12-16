using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour {

	public string characterName;
	public Image characterPortrait;

	GameObject nameBox;

	void OnEnablel(){
		// TextBoxManager.openTextEvent += DisplayNameBox;
		// TextBoxManager.closeTextEvent += CloseNameBox;
	}

	void OnDisable(){
		// TextBoxManager.openTextEvent -= DisplayNameBox;
		// TextBoxManager.closeTextEvent -= CloseNameBox;
	}

	void Start(){
		if(GetComponent<ActivateTextAtLine>()){
			//stuff
		}
	}

	



}
