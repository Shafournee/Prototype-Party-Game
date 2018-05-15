using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWinTrigger : MonoBehaviour {

	List<GameObject> WinningPlayers = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Player"){
			col.gameObject.GetComponent<FirePlayerMovement>().enabled = false;
			WinningPlayers.Add(col.gameObject);
			StartCoroutine(StopWhenGrounded(col.gameObject.GetComponentInChildren<FirePlayerEdgeDetector>()));
		}
	}

	IEnumerator StopWhenGrounded(FirePlayerEdgeDetector floorDetector) {
		while(!floorDetector.isTouching) {
			yield return null;
		}
		floorDetector.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
	}

}
