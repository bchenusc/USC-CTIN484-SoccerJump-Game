using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public int mPlayerNumber = 1;
	public int PlayerNumber { get { return mPlayerNumber; } set { mPlayerNumber = value; } }

	private event Action CollisionExit;
	

	// Player movement gameplay
	public bool mIsGrounded = true;
	public bool IsGrounded { get { return mIsGrounded; } set { mIsGrounded = value;}}

	void OnCollisionStay(Collision c) {
		if (!c.gameObject.CompareTag("Ball"))
		mIsGrounded = true;
	}

	void OnCollisionExit(Collision c) {
		mIsGrounded = false;
		if (CollisionExit != null) {
			CollisionExit();
		}
	}

	public void RegisterCollisionExit(Action func) {
		CollisionExit += func;
	}

	public void DeRegisterCollisionExit(Action func) {
		CollisionExit -= func;
	}

}