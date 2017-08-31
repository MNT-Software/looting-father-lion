using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCheck : MonoBehaviour {

	// Declared variables for use in script
	// Private
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			player.GetComponent<Rigidbody> ().useGravity = true;
			player.GetComponent<Player> ().stairs = false;
		}
		else if (other.tag == "Sound Trigger") {
			Physics.IgnoreCollision (other.GetComponent<SphereCollider> (), GetComponent<Collider> ());
		}
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Player") {
			player.GetComponent<Rigidbody> ().useGravity = false;
			player.GetComponent<Player> ().stairs = true;
		}
		else if (other.tag == "Sound Trigger") {
			Physics.IgnoreCollision (other.GetComponent<SphereCollider> (), GetComponent<Collider> ());
		}
	}
}
