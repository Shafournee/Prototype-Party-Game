using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D body;

    [SerializeField] //TODO move this to a pool of some kind!
    private GameObject ballPopPrefab;
	
    public void Launch(Vector2 shot) {
        if(body == null) {
            body = GetComponent<Rigidbody2D>();
        }

        body.AddForce(shot);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<GrabBallsPlayer>() == null) return; //TODO this is not great

        var popAnim = GameObject.Instantiate(ballPopPrefab);
        popAnim.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}
