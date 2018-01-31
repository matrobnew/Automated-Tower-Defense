using UnityEngine;
using System.Collections;

public class Bullet_Nav : MonoBehaviour {

	public float lifeTime;
	public GameObject brokenObject ;
	Transform thisTransform ;
	bool  isLive = true ;

	void Start () {

		thisTransform = this.transform ;
		Invoke ("Hit", lifeTime);
	}

	void FixedUpdate () {
	//this can contain code for raycasting, to determine proximity to an impactor and how shell should respond
	}

	void OnCollisionEnter ( Collision collision ) {
		if ( isLive ) {
			isLive = false ;
			Hit () ;
		}
	}

	void Hit () {
		if ( brokenObject ) {
			Instantiate ( brokenObject , thisTransform.position , Quaternion.identity ) ;
		}
		// Write your damage process using "hitObject" and "hitNormal".
		Destroy ( this.gameObject ) ;
	}
}
