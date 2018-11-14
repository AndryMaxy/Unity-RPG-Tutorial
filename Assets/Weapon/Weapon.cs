using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = ("RPG/Weapon"))]
	public class Weapon : ScriptableObject {

		public Transform gripTransform;

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnimation;
		[SerializeField] float minTimeBetweenHits = 1f;
		[SerializeField] float maxAttackRange = 2f;

		public float GetMinTimeBetweenHits(){
			return minTimeBetweenHits;
		}

		public float GetMaxAttackRange(){
			return maxAttackRange;
		}

		public GameObject GetWeaponPrefab(){
			return weaponPrefab;
		}

		public AnimationClip GetAttackAnimClip(){
			RemoveAnimationEvents ();
			return attackAnimation;
		}
		private void RemoveAnimationEvents(){
			// so that asset packs cannot cause errors
			attackAnimation.events = new AnimationEvent[0];
		}
	}
}
