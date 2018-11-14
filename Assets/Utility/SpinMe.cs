using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{	
	public class SpinMe : MonoBehaviour {

		[SerializeField] float xRotationsPerMinute = 1f;
		[SerializeField] float yRotationsPerMinute = 1f;
		[SerializeField] float zRotationsPerMinute = 1f;
		
		void Update () {
			float xDegreesPerFrame = xRotationsPerMinute * Time.deltaTime / 60f * 360f; // TODO COMPLETE ME
	        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

			float yDegreesPerFrame = yRotationsPerMinute * Time.deltaTime / 60f * 360f; // TODO COMPLETE ME
	        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

			float zDegreesPerFrame = zRotationsPerMinute * Time.deltaTime / 60f * 360f; // TODO COMPLETE ME
	        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
		}
	}
}