using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallExplosion : MonoBehaviour {

    public Ball ballPrefab;
    public int minBalls = 50;
    public int maxBalls = 90;
    public float minPower = 100f;
    public float maxPower = 150f;

    public void Start() {
        Invoke("Explode", 2f);
    }

    public void Explode() {
        var count = Random.Range(minBalls, maxBalls);
        for (var i = 0; i < count; i++) {
            var ball = GameObject.Instantiate<Ball>(ballPrefab);
            ball.transform.position = transform.position;

            var dir = Random.insideUnitCircle;
            var power = Random.Range(minPower, maxPower);

            ball.Launch(dir * power);
        }

        Destroy(this.gameObject);
    }
}
