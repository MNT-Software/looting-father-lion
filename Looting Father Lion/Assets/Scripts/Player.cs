using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Declared variables for use in script
	// Private
	private GameObject gameManager;
	private SphereCollider proximitySound;
	private float horizontalAxis;
	private float verticalAxis;
	private float triggerAxis;
	private Vector2 horizontalMovement;
	private Vector2 verticalMovement;

	// Public
	public float moveSpeed = 10;
	public float speedModifier;
	public float jumpHeight;
	public bool stairs;
	public bool movingHorizontal;
	public bool movingVertical;
	public bool isMoving;
	public bool isRunning;
	public bool isSneaking;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.color = new Color (0, 255, 0);
		gameManager = GameObject.Find ("Game Manager");
		proximitySound = gameObject.GetComponentInChildren<SphereCollider> ();
		stairs = false;
		movingHorizontal = false;
		movingVertical = false;
		isMoving = false;
	}
		
	// Update is called once per frame
	void Update () {

		GamepadMovement ();
		SoundTrigger ();

		// Button test (Delete me when done!)
		if (Input.GetButtonDown ("A Button")) {
			Debug.Log ("A button pressed!");
		}
		if (Input.GetButtonDown ("B Button")) {
			Debug.Log ("B button pressed!");
		}
		if (Input.GetButtonDown ("X Button")) {
			Debug.Log ("X button pressed!");
		}
		if (Input.GetButtonDown ("Y Button")) {
			Debug.Log ("Y button pressed!");
		}
		if (Input.GetButtonDown ("Left Bumper")) {
			Debug.Log ("Left bumper pressed!");
		}
		if (Input.GetButtonDown ("Right Bumper")) {
			Debug.Log ("Right bumper pressed!");
		}
		if (Input.GetButtonDown ("Select/Back")) {
			Debug.Log ("Select/back button pressed!");
		}
		if (Input.GetButtonDown ("Start")) {
			Debug.Log ("Start button pressed!");
		}
	}

	void GamepadMovement(){
		
		// Basic gamepad control scheme (doesn't really work well for keyboards!)
		// Get horizontal and trigger input axis and store in floats
		horizontalAxis = Input.GetAxis ("Gamepad Horizontal");
		verticalAxis = Input.GetAxis ("Gamepad Vertical");
		triggerAxis = Input.GetAxis ("Triggers");

		// Stop axis "smoothing" by converting axis to whole integer vector, allowing for small "deadzone"
		// Horizontal Movement
		if (horizontalAxis > 0.25) {
			horizontalMovement = new Vector2 (1, 0);
			movingHorizontal = true;
		}
		else if (horizontalAxis < -0.25) {
			horizontalMovement = new Vector2 (-1, 0);
			movingHorizontal = true;
		} 
		else if (horizontalAxis <= 0.25 && horizontalAxis >= -0.25) {
			horizontalMovement = Vector2.zero;
			movingHorizontal = false;
		}	

		// Vertical Movement
		if (verticalAxis > 0.25) {
			verticalMovement = new Vector2 (0, -1);
			movingVertical = true;
		}
		else if (verticalAxis < -0.25) {
			verticalMovement = new Vector2 (0, 1);
			movingVertical = true;
		} 
		else if (verticalAxis <= 0.25 && verticalAxis >= -0.25) {
			verticalMovement = Vector2.zero;
			movingVertical = false;
		}	

		// Check to see which trigger is held down and effect the speed of the player
		if (triggerAxis > 0.25) {
			speedModifier = 0.5f;
			isSneaking = true;
			isRunning = false;
		}
		else if (triggerAxis < -0.25) {
			speedModifier = 2f;
			isSneaking = false;
			isRunning = true;
		}
		else if (triggerAxis <= 0.25 && triggerAxis >= -0.25) {
			speedModifier = 1f;
			isSneaking = false;
			isRunning = false;
		}

		// Basic horizontal movement for player based on the horizontal axis input and which "speed modifying" trigger is held
		// Also checks to see if player is on stairs or not and adjusts the movement type accordingly 
		if(!stairs){
			GetComponent<Rigidbody> ().velocity = horizontalMovement * (moveSpeed * speedModifier);
		}
		else{
			GetComponent<Rigidbody> ().velocity = new Vector2(horizontalMovement.x, verticalMovement.y) * (moveSpeed * (speedModifier/3));
		}
	}

	void SoundTrigger(){

		// Checks to see if the player is moving and then adjusts the radius of the sound trigger volume based
		// on the type of player movement. Zeros the radius of the trigger volume if player is stationary.

		if (isSneaking && (movingHorizontal || (movingVertical && stairs))) {
			proximitySound.radius = 2.5f;
		}
		else if (isRunning && (movingHorizontal || (movingVertical && stairs))) {
			proximitySound.radius = 10f;
		}
		else if ((!isSneaking && !isRunning) && (movingHorizontal || (movingVertical && stairs))) {
			proximitySound.radius = 5f;
		}
		else {
			proximitySound.radius = 0f;
		}
	}
}