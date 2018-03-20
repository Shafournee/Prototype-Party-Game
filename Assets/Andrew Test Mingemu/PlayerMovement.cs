using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	// ----------------------------------- Fields and Properties ----------------------------------- //
	// Player Rigidbody + Other Script Components
	Rigidbody2D rb;

	// --------- Control Variables --------- //
	// Can the player stop the y velocity of their jump? This only becomes true a few frames after a jump.
	// The player can't stop their y velocity unless they actually jumped themselves first
	bool canStopJump = false;

	// --------- Other Movement --------- //
	// The force applied for left and right movement.
	Vector2 ForceToApply = Vector2.zero;


	
	//  --------- Serialized Fields: Set in Inspector ---------  //

	// Player movement constants
	[SerializeField] float RunningForce;
	[SerializeField] float JumpingForce;
	[SerializeField] float RunningTopSpeed;

	[SerializeField] PlayerEdgeDetector FloorDetector;


	// ------------------------------------------ Methods ------------------------------------------ //
	//  --------- Start ---------  //
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	//  --------- Update ---------  //
	void Update () {
		NormalMovement();
	}

	//  --------- Normal Movement State ---------  //
	void NormalMovement() {
		// --- Jumping --- //
		JumpingHandler();
		JumpStifleHandler();
		// --- Left and Right Movement --- //
		GroundMovement();
	}

	//  --------- Umbrella Movement State ---------  //
	void UmbrellaMovement() {
		canStopJump = false;
		// --- Jumping --- //
		JumpingHandler();
		JumpStifleHandler();
		// --- Left and Right Movement --- //
		GroundMovement();
	}


	//  --------- FixedUpdate ---------  //
	void FixedUpdate() {
		// Applies the Left and Right
		if(ForceToApply != Vector2.zero) {
			rb.AddForce(ForceToApply);
		}
	}

	//  --------- State Functions ---------  //
	void JumpingHandler() {
		// Initial Jump
		if(Input.GetKeyDown(KeyCode.Space) && FloorDetector.isTouching) {
			rb.AddForce(Vector2.up * JumpingForce, ForceMode2D.Impulse);
			canStopJump = true;
		}
	}

	void JumpStifleHandler() {
		// If the player is in the air, moving up, but is not holding space, immediately set the y velocity to zero.
		// This can only happen a few frames after the initial jump.
		if(!FloorDetector.isTouching && rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space) && canStopJump) {
			rb.velocity = new Vector2(rb.velocity.x, 0f);
		}
	}

	void GroundMovement() {
		// Magnitude of the running force decreases as we approach top speed, going to 0 if we're at top speed.
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			Vector2 force;
			if(Input.GetKey(KeyCode.A)) { force = Vector2.left * RunningForce; } 
			else { force = Vector2.right * RunningForce; }
			// Decrease the magnitude of the force based on current velocity (only if the input is along with the movement direction)
			bool inputInDirectionOfMotion = Mathf.Sign(force.x) == Mathf.Sign(rb.velocity.x);
			if(inputInDirectionOfMotion) {
				force /= Mathf.Max(1f, Mathf.Abs(rb.velocity.x));
			}
			// Only apply a force if we're not at top speed, or if the input direction is against the current movement direction.
			if(Mathf.Abs(rb.velocity.x) < RunningTopSpeed || !inputInDirectionOfMotion){
				ForceToApply = force;
			} else {
				// Just maintain top speed if we're already there
				rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * RunningTopSpeed, rb.velocity.y);
				ForceToApply = Vector2.zero;
			}
		} else {
			rb.velocity = new Vector2(0f, rb.velocity.y);
			ForceToApply = Vector2.zero;
		}
	}



	//  --------- Public Functions ---------  //
	// Returns true if the player is on the ground
	public bool OnGround(){
		return FloorDetector.isTouching;
	}

	// Uncaps the player's speed until they touch the ground again.
	public void UncapHSpeedUntilOnGround() {
		StartCoroutine(uncap_speed());
	}
	IEnumerator uncap_speed() {
		while(!FloorDetector.isTouching) {
			yield return null;
		}
	}


	//  --------- Helper Functions ---------  //

}