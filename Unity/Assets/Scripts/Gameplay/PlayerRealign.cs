using UnityEngine;
using System.Collections;

// Stick this on something with a HingeJoint
// For the Hinge Joint on the flipper, you want to make the axis of the
// anchor (the orange thing) perpendicular to the table

public class PlayerRealign : MonoBehaviour
{
	float mRealignForce = 1000; // up force
	Transform mRealignForcePos;
	float mODot = 0;
	Transform mCenterOfMass;
	Vector3 mOriginalCenterOfMass;

	PlayerScript pScript;

	void Start() {
		pScript = gameObject.GetComponent<PlayerScript> ();
		mRealignForcePos = transform.FindChild("UpPoint");
		mOriginalCenterOfMass = rigidbody.centerOfMass;

		// Moves the center of mass to the proper location.
		mCenterOfMass = transform.FindChild("CenterOfMass");
		rigidbody.centerOfMass = mCenterOfMass.position - transform.position;
		
	}
	
	void FixedUpdate(){
		if (pScript.IsGrounded) {
			rigidbody.AddForceAtPosition(-Mathf.Sign(transform.up.x) * OppositeDot(Vector3.up, transform.up) * mRealignForce * Vector3.right, mRealignForcePos.position);
			//rigidbody.AddTorque(-Mathf.Sign(transform.up.x) * OppositeDot(Vector3.up, transform.up) * mRealignForce * Vector3.forward, ForceMode.VelocityChange);
		}
	}
	
	float OppositeDot(Vector3 v1, Vector3 v2) {
		return 1f - Mathf.Abs(Vector3.Dot(Vector3.Normalize(v1), Vector3.Normalize(v2)));
	}

}
