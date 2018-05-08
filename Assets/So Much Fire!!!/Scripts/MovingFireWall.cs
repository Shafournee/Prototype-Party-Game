using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFireWall : MonoBehaviour {

	[SerializeField] float Delay;
	[SerializeField] float Speed;

	// Use this for initialization
	void Start () {
		StartCoroutine(MoveAfter(Delay));
	}
	
	IEnumerator MoveAfter(float second){
		yield return new WaitForSeconds(second);
		GetComponent<Rigidbody2D>().velocity = new Vector2(Speed, 0);
	}
}
