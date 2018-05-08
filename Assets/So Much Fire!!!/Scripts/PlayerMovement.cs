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

	// Is this player squished?
	public bool squished { get; private set; }

	// --------- Other Movement --------- //
	// The force applied for left and right movement.
	Vector2 ForceToApply = Vector2.zero;


	
	//  --------- Serialized Fields: Set in Inspector ---------  //

	// Player movement constants
	[SerializeField] float RunningForce;
	[SerializeField] float JumpingForce;
	[SerializeField] float RunningTopSpeed;

	// Detectors
	public PlayerEdgeDetector FloorDetector;

	// Inputs
	[SerializeField] KeyCode Left = KeyCode.A;
	[SerializeField] KeyCode Right = KeyCode.D;
	[SerializeField] KeyCode Jump = KeyCode.Space;
	[SerializeField] KeyCode Down = KeyCode.S;

	// ------------------------------------------ Methods ------------------------------------------ //
	//  --------- Start ---------  //
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		squished = false;
		StartCoroutine(HoldingS());
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
		if(Input.GetKeyDown(Jump) && FloorDetector.isTouching) {
			rb.AddForce(Vector2.up * JumpingForce, ForceMode2D.Impulse);
			canStopJump = true;
		}

		// Lets you bounce off of player's heads.
		else if(!FloorDetector.isTouching && rb.velocity.y < 0 && FloorDetector.isTouchingPlayer && !FloorDetector.PlayerTouching.squished) {
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * JumpingForce, ForceMode2D.Impulse);
			canStopJump = false;
			FloorDetector.PlayerTouching.Squish();
		}
	}

	void JumpStifleHandler() {
		// If the player is in the air, moving up, but is not holding space, immediately set the y velocity to zero.
		// This can only happen a few frames after the initial jump.
		if(!FloorDetector.isTouching && rb.velocity.y > 0 && !Input.GetKey(Jump) && canStopJump) {
			rb.velocity = new Vector2(rb.velocity.x, 0f);
		}
	}

	void GroundMovement() {
		// Magnitude of the running force decreases as we approach top speed, going to 0 if we're at top speed.
		if(Input.GetKey(Left) || Input.GetKey(Right)) {
			Vector2 force;
			if(Input.GetKey(Left)) { force = Vector2.left * RunningForce; } 
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

	// Squished this player. Called when another player steps on you.
	public void Squish() {
		squished = true;
		RunningForce /= 2f;
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2f, transform.localScale.z);
		transform.position = new Vector3(transform.position.x, transform.position.y - transform.GetComponent<SpriteRenderer>().bounds.extents.y, transform.position.z);
		StartCoroutine(squish_helper());
	}
	public IEnumerator squish_helper() {
		yield return new WaitForSeconds(3f);
		transform.position = new Vector3(transform.position.x, transform.position.y + transform.GetComponent<SpriteRenderer>().bounds.extents.y, transform.position.z);
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
		RunningForce *= 2f;
		squished = false;
	}


	// When the player holds S for some amount of time and they're on a platform, they fall through it.
	IEnumerator HoldingS() {
		while(true) {
			float time = 0;
			while(Input.GetKey(Down) && FloorDetector.isTouchingPlatform && time < 0.1f) {
				yield return null;
				time += Time.deltaTime;
			}
			if(Input.GetKey(Down) && FloorDetector.isTouchingPlatform) {
				Collider2D platform = FloorDetector.Platform;
				Collider2D thisPlayer = GetComponent<Collider2D>();
				Physics2D.IgnoreCollision(thisPlayer, platform);
				Physics2D.IgnoreCollision(FloorDetector.gameObject.GetComponent<BoxCollider2D>(), platform);
				yield return new WaitForSeconds(0.3f);
				Physics2D.IgnoreCollision(thisPlayer, platform, false);
				Physics2D.IgnoreCollision(FloorDetector.gameObject.GetComponent<BoxCollider2D>(), platform, false);
			}
			time = 0;
			yield return null;
		}
	}


	//  --------- Helper Functions ---------  //

}
