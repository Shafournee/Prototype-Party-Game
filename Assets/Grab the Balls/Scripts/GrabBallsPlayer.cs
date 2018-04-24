using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBallsPlayer : MonoBehaviour {

    private Rigidbody2D body;

    public string horizontalAxis;
    public string verticalAxis;
    public float speed = 10f;

	void Start () {
        body = GetComponent<Rigidbody2D>();	
	}
	
	void Update () {
        var x = Input.GetAxis(horizontalAxis);
        var y = Input.GetAxis(verticalAxis);
        var dir = new Vector2(x, y);
        dir.Normalize();

        body.velocity = dir * speed * Time.deltaTime;
	}
}
