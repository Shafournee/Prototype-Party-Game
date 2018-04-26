using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script attached to colliders placed on the edges of the player, to notify the player GO if a side is touching the ground/wall.
public class PlayerEdgeDetector : MonoBehaviour {
	// ----------------------------------- Fields and Properties ----------------------------------- //
	// Is this edge collider touching the ground?
	public bool isTouching { get; private set; }

	// Is this edge collider touching a player?
	public bool isTouchingPlayer { get; private set; }
	public PlayerMovement PlayerTouching { get; private set; }

	//  --------- Serialized Fields: Set in Inspector ---------  //

	// The direction that this edge collider lies on.
	public enum Direction { Top, Bottom, Left, Right }
	[SerializeField] Direction Side;

	// ------------------------------------------ Methods ------------------------------------------ //
	//  --------- OnCollisionStay ---------  //
	void OnCollisionStay2D(Collision2D collision) {
		if(collision.gameObject.tag == "Ground") {
			isTouching = true;
		}
		if(collision.gameObject.tag == "Player"){
			isTouchingPlayer = true;
			PlayerTouching = collision.gameObject.GetComponent<PlayerMovement>();
		}
	}

	//  --------- OnCollisionExit ---------  //
	void OnCollisionExit2D(Collision2D collision) {
		if(collision.gameObject.tag == "Ground") {
			isTouching = false;
		}
		if(collision.gameObject.tag == "Player"){
			isTouchingPlayer = false;
			PlayerTouching = null;
		}
	}
}
