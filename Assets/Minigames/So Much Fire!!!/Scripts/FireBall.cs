using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player") {
			Destroy(col.gameObject);
		}
	}
}
