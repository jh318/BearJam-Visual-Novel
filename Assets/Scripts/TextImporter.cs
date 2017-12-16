using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {

	public TextAsset textfile;
	public string[] textLines;

	void Start(){
		if(textfile != null){
			textLines = textfile.text.Split('\n');
		}
	}
}
