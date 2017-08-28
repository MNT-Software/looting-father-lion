using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Declared variables for use in script
	// Private
	private GameObject gameManager;
	private float horizontalAxis;
	private float triggerAxis;
	private Vector2 movement;

	// Public
	public float moveSpeed = 10;
	public float speedModifier;
	public float jumpHeight;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.color = new Color (0, 255, 0);
		gameManager = GameObject.Find ("Game Manager");
	}
		
	// Update is called once per frame
	void Update () {
		
		// Basic gamepad control scheme (doesn't really work well for keyboards!)
		// Get horizontal and trigger input axis and store in floats
		horizontalAxis = Input.GetAxis ("Gamepad Horizontal");
		triggerAxis = Input.GetAxis ("Triggers");

		// Stop axis "smoothing" by converting axis to whole integer vector, allowing for small "deadzone"
		if (horizontalAxis > 0.25) {
			movement = new Vector2 (1, 0);
		}
		else if (horizontalAxis < -0.25) {
			movement = new Vector2 (-1, 0);
		} 
		else if (horizontalAxis <= 0.25 | horizontalAxis >= -0.25) {
			movement = Vector2.zero;
		}			

		// Check to see which trigger is held down and effect the speed of the player
		if (triggerAxis > 0.25){
			speedModifier = 0.5f;
		}
		else if (triggerAxis < -0.25){
			speedModifier = 2f;
		}
		else if (triggerAxis <= 0.25 | horizontalAxis >= -0.25){
			speedModifier = 1f;
		}

		// Basic horizontal movement for player based on the horizontal axis input and which "speed modifying" trigger is held
		GetComponent<Rigidbody> ().velocity = movement * (moveSpeed * speedModifier);

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
}
