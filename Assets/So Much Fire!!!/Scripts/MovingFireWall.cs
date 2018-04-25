using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFireWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(MoveAfter(3f));
	}
	
	IEnumerator MoveAfter(float second){
		yield return new WaitForSeconds(second);
		GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 0);
	}
}
