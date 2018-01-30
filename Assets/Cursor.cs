using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

	private CameraRaycaster rayCaster;

	void Awake(){
		rayCaster = gameObject.GetComponent<CameraRaycaster>();
	}

	void Update () {
		
		Debug.Log(rayCaster.layerHit);
	}
}
