using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyAI : MonoBehaviour {

    Rigidbody2D rigidBody;
    float speed = 5f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rigidBody.velocity = new Vector2(-speed, 0f);
	}
}
