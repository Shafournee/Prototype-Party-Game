using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyPlayer : MonoBehaviour
{

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    Rigidbody2D rigidBody;
    float speed = 5f;
    public Color color;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
        }
        else if (Input.GetKey(down))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
        }
        else
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }
        if (Input.GetKey(left))
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
    }
}
