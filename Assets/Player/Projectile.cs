using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Weapons
{
	public class Projectile : MonoBehaviour {

		[SerializeField] float projectileSpeed;

		GameObject shooter;
		float damageCaused;
		const float DESTROY_DELAY = 0.01f;

		public void SetShooter(GameObject shooter){
			this.shooter = shooter;
		}

		public void SetDamage(float damage){
			damageCaused = damage;
		}

		public float GetDefaultLaunchSpeed(){
			return projectileSpeed;
		}

		void OnCollisionEnter(Collision collision){

			if (shooter && collision.gameObject.layer != shooter.layer){
				DamageIfDamageable (collision);
			}
		}

		private void DamageIfDamageable(Collision collision){
			Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));

			if (damageableComponent) {
				(damageableComponent as IDamageable).TakeDamage(damageCaused);
			}	
			Destroy (gameObject, DESTROY_DELAY);
		}
	}
}