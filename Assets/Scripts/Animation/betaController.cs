using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betaController : MonoBehaviour {


	static Animator anim;
	//setting the parameter
	public float walkSpeed = 10.0F;
	public float runSpeed = 20.0F;
	public float rotationSpeed = 100.0F;
	public int speedLevel = 0;
	public int clickTimes = 0;
	public bool isSelected = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void actorMovement(){
		if (speedLevel == 0) {
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isStanding", true);
			anim.SetBool ("isRunning", false);
		} else if (speedLevel == 1) {
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isStanding", false);
			anim.SetBool ("isRunning", false);
		} else if (speedLevel == 2) {
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isStanding", false);
			anim.SetBool ("isRunning", true);
		}
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
			if (speedLevel <= 0) {
				speedLevel = speedLevel + 1;
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isStanding", false);
				anim.SetBool ("isRunning", false);
			} else if (speedLevel >= 1) {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isStanding", false);
				anim.SetBool ("isRunning", true);
			} 
		}
		//move backward
		else if (Input.GetKeyDown(KeyCode.K)) {
			if (speedLevel <= 0) {
				speedLevel = speedLevel + 1;
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isStanding", false);
				anim.SetBool ("isRunning", false);
			} else if (speedLevel >= 1) {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isStanding", false);
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
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			speedLevel = speedLevel + 1;
			//catch the exception
			if (speedLevel < 0) {
				speedLevel = 0;
			} else if (speedLevel > 3) {
				speedLevel = 3; 
			}
			actorMovement ();
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			speedLevel = speedLevel - 1;
			if (speedLevel < 0) {
				speedLevel = 0;
			} else if (speedLevel > 3) {
				speedLevel = 3; 
			}
			actorMovement ();
		}

		//detect multiple click
		if (Input.GetMouseButtonDown (0)) {
			clickTimes = clickTimes + 1;
		}
	}

	void OnMouseDown()
	{
		print ("have click beta");
		if (!isSelected)
		{
			isSelected = true;
		}
		else
		{
			isSelected = false;
		}
	}
}
