using UnityEngine;
using System.Collections;

// Stick this on something with a HingeJoint
// For the Hinge Joint on the flipper, you want to make the axis of the
// anchor (the orange thing) perpendicular to the table

public class PlayerRealign : MonoBehaviour
{
	float force = 500; // up force
	Transform mRealignForcePos;
	float mODot = 0;

	void Start() {
		mRealignForcePos = transform.FindChild("UpPoint");
		Transform centerOfMass = transform.FindChild("CenterOfMass");
		rigidbody.centerOfMass = centerOfMass.position - transform.position;
	}
	
	void FixedUpdate(){
		mODot = OppositeDot(Vector3.up, mRealignForcePos.position - transform.position);
		rigidbody.AddForceAtPosition(ForceScale(mODot)* Vector3.up, mRealignForcePos.position);
	}

	float ForceScale(float oDot) {
		if (oDot < 0.01f) {
			return 0;
		} else {
			oDot *= 100; // Scale 
			return Mathf.Pow(oDot, 2);
		}
	}

	float OppositeDot(Vector3 v1, Vector3 v2) {
		return 1f - Mathf.Abs(Vector3.Dot(Vector3.Normalize(v1), Vector3.Normalize(v2)));
	}

}
