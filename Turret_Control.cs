using UnityEngine;
using System.Collections;


// This script must be attached to "Gunbase".

public class Turret_Control : MonoBehaviour {

	Transform thisTransform ;
	Transform rootTransform ;

	public Vector3 targetPosition;
	public Vector3 EstTargetPosition;
	Vector3 targetVelocity;
	public Vector3 direction;
	public Vector3 UpdatedDirection;
	public GameObject MyOpponent;
	int bulletVelocity = 2000; //note--this is a public property of Fire_Spawn_CS, set in Inspector, should be derived dynamically from there
	
	void Start () {
		thisTransform = this.transform ;
		rootTransform = thisTransform.root;
		MyOpponent = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {

			//auto-aim
			if (MyOpponent) {

				targetPosition = MyOpponent.transform.position;  //get the current position of target
				targetVelocity = MyOpponent.GetComponent<Rigidbody> ().velocity;  //get the current velocity of target
				direction = targetPosition - thisTransform.position;  //what direction to aim toward at this current instant

				float time = FirstOrderInterceptTime (bulletVelocity, direction, targetVelocity); 
				//new function below calculates time for bullet to traverse to estimated new target position, based on inputs 
				
				//estimate new target position after bullet travel time
				EstTargetPosition = targetPosition + (time * targetVelocity);

				//now adjust the aiming direction accordingly
				UpdatedDirection = EstTargetPosition - thisTransform.position;
				//aim
                //we want JUST the y-axis aim for the turret (y axis happens to be x in this config)
                UpdatedDirection.y = 0;
				thisTransform.rotation = Quaternion.Slerp (thisTransform.rotation, Quaternion.LookRotation (UpdatedDirection), 0.1f);
				
			}
	}


	//first-order intercept using relative target position
	public static float FirstOrderInterceptTime
	(
		float shotSpeed,
		Vector3 targetRelativePosition,
		Vector3 targetRelativeVelocity
	) {
		float velocitySquared = targetRelativeVelocity.sqrMagnitude;
		if(velocitySquared < 0.001f)
			return 0f;

		float a = velocitySquared - shotSpeed*shotSpeed;

		//handle similar velocities
		if (Mathf.Abs(a) < 0.001f)
		{
			float t = -targetRelativePosition.sqrMagnitude/
				(
					2f*Vector3.Dot
					(
						targetRelativeVelocity,
						targetRelativePosition
					)
				);
			return Mathf.Max(t, 0f); //don't shoot back in time
		}

		float b = 2f*Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
		float c = targetRelativePosition.sqrMagnitude;
		float determinant = b*b - 4f*a*c;

		if (determinant > 0f) { //determinant > 0; two intercept paths (most common)
			float	t1 = (-b + Mathf.Sqrt(determinant))/(2f*a),
			t2 = (-b - Mathf.Sqrt(determinant))/(2f*a);
			if (t1 > 0f) {
				if (t2 > 0f)
					return Mathf.Min(t1, t2); //both are positive
				else
					return t1; //only t1 is positive
			} else
				return Mathf.Max(t2, 0f); //don't shoot back in time
		} else if (determinant < 0f) //determinant < 0; no intercept path
			return 0f;
		else //determinant = 0; one intercept path, pretty much never happens
			return Mathf.Max(-b/(2f*a), 0f); //don't shoot back in time
	}

}