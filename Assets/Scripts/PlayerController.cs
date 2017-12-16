using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 2.0f;
	public bool canMove;

	Rigidbody2D body;
	Animator anim;
	SpriteRenderer spriteRend;

	void Start(){
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRend = GetComponentInChildren<SpriteRenderer>();
		spriteRend.flipX = true;
	}

	void Update(){
		if(!canMove){
			StopPlayerMovement();
			return;
		} 
		GetPlayerInput();
	}

	void GetPlayerInput(){
		
		float directionX = Input.GetAxisRaw("Horizontal");
		float directionY = Input.GetAxisRaw("Vertical");

		if(Mathf.Abs(directionX) > 0.5f){
			anim.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
			anim.SetFloat("vertical", 0.0f);
			anim.SetBool("isMoving", true);
			if(directionX < -0.5f) 
				spriteRend.flipX = true;
			else 
				spriteRend.flipX = false;
			body.velocity = new Vector2(directionX, 0) * speed ;
		}
		else if(Mathf.Abs(directionY) > 0.5f){
			anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
			anim.SetFloat("horizontal", 0.0f);
			anim.SetBool("isMoving", true);
			body.velocity = new Vector2(0, directionY) * speed;
		}
		else{
			anim.SetBool("isMoving", false);
			body.velocity = Vector2.zero;
		}

		anim.SetFloat("lastHorizontal", anim.GetFloat("horizontal"));
		anim.SetFloat("lastVertical", anim.GetFloat("vertical"));
	}

	void StopPlayerMovement(){
		body.velocity = Vector3.zero;
		anim.SetFloat("vertical", 0.0f);
		anim.SetFloat("horizontal", 0.0f);
		anim.SetBool("isMoving", false);
		// anim.SetFloat("lastVertical", 0.0f);
		// anim.SetFloat("lastHorizontal", 0.0f);
		if(anim.GetFloat("lastHorizontal") > 0.5f)
			anim.Play("Idle_Right");
		else
			anim.Play("Idle_Left");
	}
}
