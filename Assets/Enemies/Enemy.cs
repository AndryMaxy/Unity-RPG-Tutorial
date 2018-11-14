using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using RPG.Core;
using RPG.Weapons;

namespace RPG.Characters
{
	public class Enemy : MonoBehaviour, IDamageable {

		[SerializeField] float maxHealthPoints = 100f;

		[SerializeField] float damagePerShot = 2f;
		[SerializeField] float secondsBetweenShots = 0.5f;
		[SerializeField] float attackRadius = 5f;

		[SerializeField] float moveRadius = 20f;

		[SerializeField] GameObject projectileToUse;
		[SerializeField] GameObject projectileSocket;

		bool isAttacking = false;
		float currentHealthPoints;
		AICharacterControl aiCharacterControl = null;
		GameObject player = null;

		public float healthAsPercentage {
			get{ 
				return currentHealthPoints / maxHealthPoints;
			}
		}

		void Start(){
			player = GameObject.FindGameObjectWithTag ("Player");
			aiCharacterControl = GetComponent <AICharacterControl> ();
			currentHealthPoints = maxHealthPoints;
		}

		void Update(){
			float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);

			if (distanceToPlayer <= attackRadius && !isAttacking) {
				isAttacking = true;
				InvokeRepeating ("FireProjectile", 0f, secondsBetweenShots);
			} 

			if (distanceToPlayer > attackRadius) {
				CancelInvoke ();
				isAttacking = false;
			}
				
			if (distanceToPlayer <= moveRadius) {
				aiCharacterControl.SetTarget (player.transform);
			} else {
				aiCharacterControl.SetTarget (transform);		
			}
		}

		public void TakeDamage(float damage){
			currentHealthPoints = Mathf.Clamp (currentHealthPoints - damage, 0f, maxHealthPoints);
			if (currentHealthPoints <= 0) {
				Destroy (gameObject);
			}
		}

		void FireProjectile(){
			GameObject  newProjectile = Instantiate (projectileToUse, projectileSocket.transform.position, Quaternion.identity);
			Projectile projectileComponent = newProjectile.GetComponent<Projectile> ();
			projectileComponent.SetDamage(damagePerShot);
			projectileComponent.SetShooter(gameObject);

			Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
			float projectileSpeed = projectileComponent.GetDefaultLaunchSpeed();
			newProjectile.GetComponent<Rigidbody> ().velocity = unitVectorToPlayer * projectileSpeed;
		}

		void Hit(){}

		void OnDrawGizmos(){
			Gizmos.color = new Color (255f, 0f, 0f, 0.5f);
			Gizmos.DrawWireSphere (transform.position, attackRadius);
			Gizmos.color = new Color (0f, 0f, 255f, 0.5f);
			Gizmos.DrawWireSphere (transform.position, moveRadius);
		}
	}
}