using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
	public class CameraFollow : MonoBehaviour {

		GameObject player;
		private float minFov = 35f;
		private float maxFov = 70f;
		private float sensitivity = 0.5f;
		private float currentFieldOfView;
		private float targetFieldOfView;

		void Start(){
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		void LateUpdate () {
			transform.position = player.transform.position;
		}

		void Update(){
			if (Input.GetKey(KeyCode.A)) {
				this.transform.Rotate(new Vector3(0f, -2.0f, 0.0f));
			}
			if (Input.GetKey(KeyCode.D)) {
				this.transform.Rotate(new Vector3(0f, 2.0f, 0.0f));
			}
			if (Input.GetKey(KeyCode.S)) {
				currentFieldOfView = Camera.main.fieldOfView;
				targetFieldOfView = currentFieldOfView + (1f * sensitivity);
				Camera.main.fieldOfView = Mathf.Clamp(targetFieldOfView, minFov, maxFov);
			}
			if (Input.GetKey(KeyCode.W)) {
				currentFieldOfView = Camera.main.fieldOfView;
				targetFieldOfView = currentFieldOfView - (1f * sensitivity);
				Camera.main.fieldOfView = Mathf.Clamp(targetFieldOfView, minFov, maxFov);
			}
		}
	}
}