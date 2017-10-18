using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lilyController : MonoBehaviour {


	static Animator anim;
	//setting the parameter
	public float walkSpeed = 10.0F;
	public float runSpeed = 20.0F;
	public float rotationSpeed = 100.0F;
	public int speedLevel = 0;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//float translation = Input.GetAxis ("Vertical") * runSpeed;
		//float ratation = Input.GetAxis ("Horizontal") * rotationSpeed;
		//Jump
		if (Input.GetButtonDown ("Jump")) {
			anim.SetTrigger ("isJumping");
		}
		//move forward
		else if (Input.GetKeyDown(KeyCode.I)) {
			if (speedLevel == 0) {
				speedLevel = speedLevel + 1;
				anim.SetBool ("isWalking", true);
			} else if (speedLevel == 1) {
				anim.SetBool ("isWalking", true);
			} else if (speedLevel == 2) {
				anim.SetBool ("isRunning", true);
			}
		}
		//move backward
		else if (Input.GetKeyDown(KeyCode.K)) {
			if (speedLevel == 0) {
				speedLevel = speedLevel + 1;
				anim.SetBool ("isWalking", true);
			} else if (speedLevel == 1) {
				anim.SetBool ("isWalking", true);
			} else if (speedLevel == 2) {
				anim.SetBool ("isRunning", true);
			}
		} 
		//turn left
		else if (Input.GetKeyDown(KeyCode.J)) {
			anim.SetTrigger ("isTurnLeft");
		}
		//turn right
		else if (Input.GetKeyDown(KeyCode.L)) {
			anim.SetTrigger ("isTurnRight");
		}
		//Change the speed
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
			speedLevel = speedLevel + 1;
			//catch the exception
			if (speedLevel < 0) {
				speedLevel = 0;
			} else if (speedLevel > 3) {
				speedLevel = 3; 
			}
		} else if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
			speedLevel = speedLevel - 1;
			if (speedLevel < 0) {
				speedLevel = 0;
			} else if (speedLevel > 3) {
				speedLevel = 3; 
			}
		}
			
		//operate the charactor by point-n-click 
		if (Input.GetMouseButtonDown (0)) {
			anim.SetTrigger ("isJumping");
		}
	}
}
