using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_CameraMovement : MonoBehaviour {

	[SerializeField] float Speed = 0;

	[SerializeField] GameObject[] Players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 
	void CalculateCameraLocations() {
		Vector3 average = Vector3.zero;
		for(int i = 0; i < Players.Length; i++) {
			average += Players[i].transform.position;
		}
		average /= Players.Length;


	}
}
