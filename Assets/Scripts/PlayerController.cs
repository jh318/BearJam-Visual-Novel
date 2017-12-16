using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 2.0f;
	public bool canMove;

	Rigidbody2D body;

	void Start(){
		body = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if(!canMove) return;
		GetPlayerInput();
	}

	void GetPlayerInput(){
		if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f){
			body.velocity = new Vector2(Input.GetAxisRaw("Horizontal"),0) * speed ;
		}
		else if(Input.GetAxisRaw("Vertical") > 0.5f){
			//body.AddForce(new Vector2(0,1) * 1000);
		}
	}
}
