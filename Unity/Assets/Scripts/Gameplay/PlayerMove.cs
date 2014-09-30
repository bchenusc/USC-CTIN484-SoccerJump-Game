using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : MonoBehaviour {
	// Dependencies
	Transform mRealignForcePos;
	PlayerScript pScript;

	// Value will be set through InputManager.
	private float mMinJumpPower = 0;

	// User control force
	private float mUserForce = 1250;

	// Use this for initialization
	void Start () {
		RegisterButtons ();
		pScript = gameObject.GetComponent<PlayerScript> ();
		mRealignForcePos = transform.FindChild ("UpPoint");

		// Variable initialization.
		mMinJumpPower = SingletonObject.Get.getInputManager().MinJumpPower;
	}

	void RegisterButtons() {
		SingletonObject.Get.getInputManager().RegisterOnKeyHeld (OnKeyHeld);
		SingletonObject.Get.getInputManager().RegisterOnKeyHeld (OnKeyDown);
	}

	void DeRegisterButtons() {
		SingletonObject.Get.getInputManager().DeregisterOnKeyHeld (OnKeyHeld);
		SingletonObject.Get.getInputManager().DeregisterOnKeyHeld (OnKeyDown);
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}

	private void OnKeyDown(KeyCode key) {
		if (pScript.IsGrounded == false)
			return;
			InputManager inputManager = SingletonObject.Get.getInputManager();
//		if (pScript.PlayerNumber == 1) {
//			// Jump
//			if (key == inputManager.key_p1_jump) {
//				Jump();
//				return;
//			}
//			return;
//		} else {
//			if (pScript.PlayerNumber == 2) {
//				// Jump
//				if (key == inputManager.key_p2_jump) {
//					Jump();
//					return;
//				}
//				return;
//			}
//		}
	}


	private void OnKeyHeld(KeyCode key) {


//		if (pScript.PlayerNumber == 1) {
//			// Jump
//			InputManager inputManager = SingletonObject.Get.getInputManager();
//			if (key == inputManager.key_p1_jump) {
//				Jump();
//				return;
//			}
//			// Tilt forward
//			if (key == inputManager.key_p1_tilt[0]) {
//				AddForceInDirection(Vector3.forward);
//				return;
//			}
//			// Tilt backward
//			if (key == inputManager.key_p1_tilt[1]) {
//				AddForceInDirection(-Vector3.forward);
//				return;
//			}
//			// Tilt left
//			if (key == inputManager.key_p1_tilt[2]) {
//				AddForceInDirection(-Vector3.right);
//				return;
//			}
//			// Tilt right
//			if (key == inputManager.key_p1_tilt[3]) {
//				AddForceInDirection(Vector3.right);
//				return;
//			}
//			return;
//		}
//		if (pScript.PlayerNumber == 2) {
//			InputManager inputManager = SingletonObject.Get.getInputManager();
//			// Jump
//			if (key == inputManager.key_p2_jump) {
//				Jump();
//				return;
//			}
//
//			// Tilt left
//			if (key == inputManager.key_p2_tilt[2]) {
//				AddForceInDirection(-Vector3.right);
//				return;
//			}
//			// Tilt right
//			if (key == inputManager.key_p2_tilt[3]) {
//				AddForceInDirection(Vector3.right);
//				return;
//			}
//			return;
//		}

	}

	private void AddForceInDirection(Vector3 direction) {
		rigidbody.AddForceAtPosition(mUserForce * direction, mRealignForcePos.position);
	}

	private void Jump() {
		if (!pScript.IsGrounded) return; // Must be grounded to jump.
		// Jump in the direction of the up vector.
		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",null,0.1f,false, 0, null);
	}

}












