using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_CameraMovement : MonoBehaviour {

	// All players in the game.
	[SerializeField] GameObject[] Players;

	// The camera attached to this GO
	Camera camera;

	// Target positions/zooms
	Vector3 TargetPosition;
	float TargetZoom;

	// Minimum Zoom value
	const float MinimumZoomValue = 8;

	// Buffer zone sizes for the X and Y edges of the camera, in unity units.
	// Barriers where the camera should zoom out when a player is outside of it, measured as offsets from edges
	static readonly Vector2 PushOutBarrier = new Vector2(8f, 3f);
	// Barriers where the camera should zoom in when all players are within it, measured as offsets from edges
	static readonly Vector2 PullInBarrier = new Vector2(12f, 5f);

	// Velocities for lerping functions
	Vector3 posVelocity = Vector3.zero;
	float zoomVelocity = 0;



	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		TargetPosition = transform.position;
		TargetZoom = camera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateCameraLocations();
	}

	// Late Update
	void LateUpdate() {
		LerpToTargets();
	}

	// 
	void CalculateCameraLocations() {
		Vector3 cameraPos = transform.position;
		// Get target position
		Vector3 average = Vector3.zero;
		int length = 0;
		foreach(GameObject player in Players){
			if(player != null) {
				average += player.transform.position;
				length++;
			}
		}
		if(length != 0){
			average /= length;
			TargetPosition = new Vector3(average.x, cameraPos.y, cameraPos.z);
		}
		// Get target zoom
		if(length < 2) {
			TargetZoom = MinimumZoomValue;
			return;
		}
		float halfHeight = camera.orthographicSize;
		float halfWidth = halfHeight * camera.aspect;
		float left = cameraPos.x - halfWidth;
		float right = cameraPos.x + halfWidth;
		float top = cameraPos.y + halfHeight;
		float bottom = cameraPos.y - halfHeight;
		float additionalDistanceX = 0f;
		float additionalDistanceY = 0f;
		foreach(GameObject player in Players){
			if(player != null) {
				Vector3 playerPos = player.transform.position;
				// Zoom Out, X
				if(playerPos.x < left + PushOutBarrier.x) {
					additionalDistanceX = Mathf.Max(additionalDistanceX, left + PushOutBarrier.x - playerPos.x);
				} else if(playerPos.x > right - PushOutBarrier.x) {
					additionalDistanceX = Mathf.Max(additionalDistanceX, playerPos.x - (right - PushOutBarrier.x));
				}
				// Zoom Out, Y
				if(playerPos.y < bottom + PushOutBarrier.y) {
					additionalDistanceY = Mathf.Max(additionalDistanceY, bottom + PushOutBarrier.y - playerPos.y);
				} else if(playerPos.y > top - PushOutBarrier.y) {
					additionalDistanceY = Mathf.Max(additionalDistanceY, playerPos.y - (top - PushOutBarrier.y));
				}

				// Zoom Ins (only zooms in if nobody is trying to zoom out)
				if(additionalDistanceX <= 0f && additionalDistanceY <= 0f){
					// Zoom In, X 
					if(playerPos.x > left + PullInBarrier.x && playerPos.x < right - PullInBarrier.x) {
						additionalDistanceX = Mathf.Max(left + PullInBarrier.x - playerPos.x, playerPos.x - (right - PullInBarrier.x));
					}
					// Zoom In, Y
					if(playerPos.y > bottom + PullInBarrier.y && playerPos.y < top - PullInBarrier.y) {
						additionalDistanceY = Mathf.Max(bottom + PullInBarrier.y - playerPos.y, playerPos.y - (top - PullInBarrier.y));
					}
				}
			}
		}
		if(additionalDistanceX > additionalDistanceY) {
			TargetZoom = camera.orthographicSize + additionalDistanceX * (1f / camera.aspect);
		} else {
			TargetZoom = camera.orthographicSize + additionalDistanceY;
		}
		if(Mathf.Abs(camera.orthographicSize - TargetZoom) < 0.1f) {
			TargetZoom = camera.orthographicSize;
		}
		if(TargetZoom < MinimumZoomValue) {
			TargetZoom = MinimumZoomValue;
		}
	}


	// Lerps current position/zoom to target position/zoom
	void LerpToTargets() {
		transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref posVelocity, 0.5f);
		camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, TargetZoom, ref zoomVelocity, 0.5f);
	}
}
