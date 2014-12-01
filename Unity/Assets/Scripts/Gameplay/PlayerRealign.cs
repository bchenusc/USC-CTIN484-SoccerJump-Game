using UnityEngine;
using System.Collections;

// Stick this on something with a HingeJoint
// For the Hinge Joint on the flipper, you want to make the axis of the
// anchor (the orange thing) perpendicular to the table

public class PlayerRealign : MonoBehaviour
{
	float mRealignForce = 1100; // up force
	Transform mCenterOfMass;

	PlayerScript pScript;

	void Start() {
		pScript = gameObject.GetComponent<PlayerScript> ();

		// Moves the center of mass to the proper location.
		mCenterOfMass = transform.FindChild("CenterOfMass");
		rigidbody.centerOfMass = mCenterOfMass.position - transform.position;
		
	}
	
	void FixedUpdate(){
		if (pScript.IsGrounded) {
			rigidbody.AddTorque (Mathf.Sign(transform.up.x) * OppositeDot(Vector3.up, transform.up) * Vector3.forward * mRealignForce);
		}
	}
	
	float OppositeDot(Vector3 v1, Vector3 v2) {
		return 1f - Mathf.Abs(Vector3.Dot(Vector3.Normalize(v1), Vector3.Normalize(v2)));
	}

}
