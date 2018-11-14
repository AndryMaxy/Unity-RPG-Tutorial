using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI{
	[RequireComponent(typeof(CameraRaycaster))]
	public class CursorAffordance : MonoBehaviour {

		[SerializeField] Texture2D walkCursor = null;
		[SerializeField] Texture2D combatCursor = null;
		[SerializeField] Texture2D unknownCursor = null;
		[SerializeField] Vector2 cursorHotspot = new Vector2(1, 1); // depends on design/size of image

		[SerializeField] const int walkableLayerNumber = 8;
		[SerializeField] const int enemyLayerNumber = 9;

		private CameraRaycaster cameraRaycaster;

		void Awake(){
			cameraRaycaster = GetComponent<CameraRaycaster>();
		}

		void Start(){
			cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged; // Adds handler to list of functions to call
		}

		void OnLayerChanged(int newLayer){
			switch(newLayer){
			case walkableLayerNumber:
				Cursor.SetCursor (walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			case enemyLayerNumber:
				Cursor.SetCursor (combatCursor, cursorHotspot, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor (unknownCursor, cursorHotspot, CursorMode.Auto);
				break;
			}
		}
	}
}