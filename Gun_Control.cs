using UnityEngine;
using System.Collections;
using UnityEngine.UI ;

using UnityStandardAssets.CrossPlatformInput ;

// This script must be attached to gun object

public class Gun_Control : MonoBehaviour {
	Transform thisTransform ;
	Transform rootTransform ;
	Turret_Control turretScript;
	public Vector3 EstTargetPosition;
	public Vector3 UpdatedDirection;
	public GameObject MyOpponent;

	void Start () {
		thisTransform = this.transform ;
		rootTransform = thisTransform.root;
		turretScript = rootTransform.gameObject.GetComponentInChildren<Turret_Control>();
	}	

	void FixedUpdate () {

		if (turretScript) {
			MyOpponent = turretScript.MyOpponent;
		}
			//auto-aim
			if (MyOpponent) {
				//get the estimation logic from turretScript
				EstTargetPosition = turretScript.EstTargetPosition;
				
				//now adjust the aiming direction accordingly
				UpdatedDirection = EstTargetPosition - thisTransform.position;
				
				//aim, now Slerping along all dimensions
				thisTransform.rotation = Quaternion.Slerp (thisTransform.rotation, Quaternion.LookRotation (UpdatedDirection), 0.1f);
			}
	}
}