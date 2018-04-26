using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireGenerator : MonoBehaviour {

	// Fireball Prefab.
	[SerializeField] GameObject FireBallPrefab;

	// Possible spawn locations
	[SerializeField] GameObject[] SpawnLocations;

	// Delay for when the generator should start spawning fireballs.
	[SerializeField] float Delay;

	// How long between each spawn.
	[SerializeField] float Interval;

	// Speed of fireballs.
	const float FireballSpeed = 4;

	// Use this for initialization
	void Start () {
		StartCoroutine(GenerateFireballs());
	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator GenerateFireballs() {
		if(Delay != 0) yield return new WaitForSeconds(Delay);
		while(true) {
			GameObject ball = Instantiate(FireBallPrefab);
			GameObject location = SpawnLocations[Random.Range(0, SpawnLocations.Length)];
			ball.transform.position = new Vector3(location.transform.position.x, location.transform.position.y);
			ball.GetComponent<Rigidbody2D>().velocity = new Vector2(-FireballSpeed, 0);
			yield return new WaitForSeconds(Interval);
		}
	}
}
