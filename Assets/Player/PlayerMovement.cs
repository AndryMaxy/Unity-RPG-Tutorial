using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Character
{
	[RequireComponent(typeof (NavMeshAgent))]
	[RequireComponent(typeof (AICharacterControl))]
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class PlayerMovement : MonoBehaviour
	{

	//	[SerializeField] float walkMoveStopRadius = 0.2f;
	//	[SerializeField] float attackMoveStopRadius = 1f;
	//    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
	    CameraRaycaster cameraRaycaster;
	//	Vector3 currentDestination, clickPoint;
		AICharacterControl aiCharacterControl = null;
		GameObject walkTarget = null;

	        
		[SerializeField] const int walkableLayerNumber = 8;
		[SerializeField] const int enemyLayerNumber = 9;

	    void Start()
	    {
			cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
	//        m_Character = GetComponent<ThirdPersonCharacter>();
	//		currentDestination = transform.position;
			aiCharacterControl = GetComponent<AICharacterControl> ();

			cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
			walkTarget = new GameObject ("walkTarget");
	    }

		void ProcessMouseClick(RaycastHit raycastHit, int layerHit){
			switch (layerHit) {
			case enemyLayerNumber:
				GameObject enemy = raycastHit.collider.gameObject;
				aiCharacterControl.SetTarget (enemy.transform);
				break;
			case walkableLayerNumber:
				walkTarget.transform.position = raycastHit.point;
				aiCharacterControl.SetTarget (walkTarget.transform);
				break;
			}
		}
	}
}
// Fixed update is called in sync with physics
//    private void FixedUpdate()
//    {
//		clickPoint = cameraRaycaster.hit.point;
//        if (Input.GetMouseButton(0))
//        {	
//			switch(cameraRaycaster.layerHit)
//			{
//			case Layer.Walkable:
//				currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
//				break;
//			case Layer.Enemy:
//				currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
//				break;
//			default:
//				print ("RaycastEnd");
//				return;
//			}
//        }
//
//		WalkToDestination();
//    }
//void WalkToDestination (){
//	var playerToClickPoint = currentDestination - transform.position;
//	if (playerToClickPoint.magnitude >= 0) {
//		m_Character.Move (playerToClickPoint, false, false);
//	}
//	else {
//		m_Character.Move (Vector3.zero, false, false);
//	}
//}
//
//Vector3 ShortDestination(Vector3 destination, float shortening){
//	Vector3 reductionVector = (destination - transform.position).normalized * shortening;
//	return destination - reductionVector;
//}
//	void OnDrawGizmos(){
//
//		// Draw movement gizmos
//		Gizmos.color = Color.black;
//		Gizmos.DrawLine (transform.position, currentDestination);
//		Gizmos.DrawSphere (currentDestination, 0.1f);
//		Gizmos.DrawSphere (clickPoint, 0.2f);
//
//		Gizmos.color = new Color (255f, 0f, 0, 0.5f);
//		Gizmos.DrawWireSphere (transform.position, attackMoveStopRadius);
//	}