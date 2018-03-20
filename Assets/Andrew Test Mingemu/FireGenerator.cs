using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGenerator : MonoBehaviour {

	[SerializeField] GameObject FireBall;

	Text myObject;

	// Use this for initialization
	void Start () {
		StartCoroutine(GenerateFireballs());
	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator GenerateFireballs() {
		while(true) {
			GameObject ball = Instantiate(FireBall);
			float x = transform.position.x + 20;
			float y;
			if(Random.value > 0.5f) {
				y = transform.position.y - 5.5f;
			} else {
				y = transform.position.y - 1;
			}
			ball.transform.position = new Vector3(x, y);
			ball.GetComponent<FireBall>().Speed = -4;
			yield return new WaitForSeconds(2f);
		}
	}
}
