using UnityEngine;
using System.Collections;

// This script must be attached to "Fire_Point".

public class Fire_Spawn: MonoBehaviour{
	public GameObject firePrefab ;
	public GameObject bulletPrefab ;
	float bulletVelocity = 2000.0f ;     
	public float spawnOffset = 1.0f ;
	public GameObject target;
    
	Turret_Control turretScript;
	Vector3 EstTargetPosition;
	Vector3 targetposition;

	Transform thisTransform ;
	float range;

	void Start () {
		thisTransform = this.transform ;
		turretScript = thisTransform.root.gameObject.GetComponentInChildren<Turret_Control>();

		if (turretScript) {
			target = turretScript.MyOpponent;
		}
	}

	void Fire () {
		// Muzzle Fire
		if ( firePrefab ) {
			GameObject fireObject = Instantiate ( firePrefab , thisTransform.position , thisTransform.rotation ) as GameObject ;
			fireObject.transform.parent = thisTransform ;
		}
		if ( bulletPrefab ) {

			if (turretScript) {
				targetposition = turretScript.targetPosition;
				EstTargetPosition = turretScript.EstTargetPosition;						
			} 

			range = Vector3.Distance (EstTargetPosition, thisTransform.position);
			//float curRange = Vector3.Distance (targetposition, thisTransform.position);
			 
			GameObject bulletObject = Instantiate (bulletPrefab, thisTransform.position + thisTransform.forward * spawnOffset, thisTransform.rotation) as GameObject;
			bulletObject.GetComponent<Bullet_Nav> ().lifeTime = range / bulletVelocity; 
			bulletObject.GetComponent < Rigidbody > ().velocity = thisTransform.forward * bulletVelocity;	
		}

	}
}
