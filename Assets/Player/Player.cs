using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI;
using RPG.Core;
using RPG.Weapons;

namespace RPG.Characters
{
	[RequireComponent(typeof(CameraRaycaster))]
	public class Player : MonoBehaviour, IDamageable {
		[SerializeField] float maxHealthPoints = 100f;
		[SerializeField] float damagePerHit = 10f;
		[SerializeField] int enemyLayer = 9;
		[SerializeField] Weapon equippedWeapon;
		[SerializeField] AnimatorOverrideController animatorOverrideController;

		Animator animator;
		GameObject currentTarget;
		float currentHealthPoints;
		CameraRaycaster cameraRaycaster;
		float lastHitTime = 0f;
		Vector3 currentPos;

		void Start(){
			SetCurrentMaxHealth ();
			RegisterForMouseClick ();
			EquippedWeapon ();
			OverrideAnimatorController ();
		}

		void OverrideAnimatorController (){
			animator = GetComponent<Animator>();
			animator.runtimeAnimatorController = animatorOverrideController;
			animatorOverrideController ["DEFAULT ATTACK"] = equippedWeapon.GetAttackAnimClip();
		}

		void SetCurrentMaxHealth(){
			currentHealthPoints = maxHealthPoints;
		}

		void RegisterForMouseClick(){
			cameraRaycaster = FindObjectOfType<CameraRaycaster> ();
			cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
		}

		void EquippedWeapon(){
			GameObject dominantHand = RequestDominantHand ();
			var weaponPrefab = equippedWeapon.GetWeaponPrefab();
			var weapon = Instantiate (weaponPrefab, dominantHand.transform);
			weapon.transform.localPosition = equippedWeapon.gripTransform.localPosition;
			weapon.transform.localRotation = equippedWeapon.gripTransform.localRotation;
		}

		private GameObject RequestDominantHand(){
			var dominantHands = GetComponentsInChildren<DominantHand> ();
			int numberOfDominantHands = dominantHands.Length;
			Assert.AreNotEqual (numberOfDominantHands, 0, "No dominant hand script found");
			Assert.IsFalse (numberOfDominantHands > 1, "Multiple dominant hand scripts found");
			return dominantHands [0].gameObject;
		}

		void OnMouseClick(RaycastHit raycastHit, int layerHit){
			if (layerHit == enemyLayer) {
				currentTarget = raycastHit.collider.gameObject;

				if (IsTargetInRange ()) {
					AttackTarget ();
				}					
			}	
		}

		private void AttackTarget(){
			var enemy = currentTarget.GetComponent<Enemy> ();
			if (Time.time - lastHitTime > equippedWeapon.GetMinTimeBetweenHits()) {
				animator.SetTrigger ("Attack");
				enemy.TakeDamage (damagePerHit);
				lastHitTime = Time.time;
			}
		}

		private bool IsTargetInRange(){
			float distanceToTarget = (currentTarget.transform.position - transform.position).magnitude;
			return (distanceToTarget <= equippedWeapon.GetMaxAttackRange());
		}

		public float healthAsPercentage{ get { 
				return currentHealthPoints / maxHealthPoints;
			}
		}

		public void TakeDamage(float damage){
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
		}
	}
}