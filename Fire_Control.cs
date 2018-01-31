using UnityEngine;
using System.Collections;

using UnityStandardAssets.CrossPlatformInput;

// This script must be attached to "Guns" object.

public class Fire_Control : MonoBehaviour {

 	//float recoilForce = 500.0f ;
	Transform thisTransform ;
	public GameObject Target;
	//public GameObject RightGun;
	//public GameObject LeftGun;
	//Rigidbody LeftGunRigidbody ;
	//Rigidbody RightGunRigidbody;
	private float timer;
	int MaxRange = 5000;
	int MinFireAlt = 150;
	void Start () {
		thisTransform = this.transform;
		Target = GameObject.FindGameObjectWithTag ("Player");
		//LeftGunRigidbody = LeftGun.gameObject.GetComponent<Rigidbody>();
		//RightGunRigidbody = RightGun.gameObject.GetComponent<Rigidbody>();
		timer = 0.0f;
	}
	
	void Update () {

			if (Target) {
				Vector3 FCdirection = Target.transform.position - thisTransform.position;
				if (Vector3.Distance (Target.transform.position, thisTransform.position) < MaxRange && Target.transform.position.y > MinFireAlt) {
					//fire on a timer
					timer = timer + 0.05f;					
					if (timer > 3.0f) {
						BroadcastMessage ("Fire", SendMessageOptions.DontRequireReceiver);
						timer = 0.0f;   //reset timer to 0
					}
				}
			}
	}

	void Fire () {
		//RightGunRigidbody.AddForceAtPosition (-thisTransform.forward * recoilForce, thisTransform.position, ForceMode.Impulse);
		//LeftGunRigidbody.AddForceAtPosition (-thisTransform.forward * recoilForce, thisTransform.position, ForceMode.Impulse);
	}
}
