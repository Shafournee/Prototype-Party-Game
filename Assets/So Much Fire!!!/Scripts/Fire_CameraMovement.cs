using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_CameraMovement : MonoBehaviour {

	//[SerializeField] float Speed = 0;

	[SerializeField] GameObject[] Players;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CalculateCameraLocations();
	}

	// 
	void CalculateCameraLocations() {
		Vector3 average = Vector3.zero;
		int length = 0;
		for(int i = 0; i < Players.Length; i++) {
			if(Players[i] != null) {
				average += Players[i].transform.position;
				length++;
			}
		}
		if(length != 0){
			average /= length;
			transform.position = new Vector3(average.x, transform.position.y, transform.position.z);
		}
	}
}
