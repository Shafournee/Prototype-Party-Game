using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

	public float Speed = 0;

	void Start() {
		if(Speed != 0) GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			Destroy(col.gameObject);
		}
	}


}
