using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : MonoBehaviour {
	string mID;

	// Globals
	//private Timer timerManager;
	//private InputManager inputManager;

	// Dependencies
	Transform mRealignForcePos;
	PlayerScript pScript;

	// Value will be set through InputManager.
	private float mMinJumpPower = 0;
	private float mExtraLiftPower = 0;
	private int mExtraLiftRepetition = 0;

	// User control force
	private float mUserForce = 1500;
	private bool allowJump = true;


	// Use this for initialization
	void Start () {
		RegisterButtons ();
		mID = gameObject.GetInstanceID().ToString();
		//SingletonObject singleton = SingletonObject.Get;
		//Debug.Log (singleton);
		//inputManager = singleton.getInputManager();
		//timerManager = singleton.getTimer();
		pScript = gameObject.GetComponent<PlayerScript> ();
		mRealignForcePos = transform.FindChild ("UpPoint");



		// Variable initialization.
		mMinJumpPower = SingletonObject.Get.getInputManager().MinJumpPower;
		//mExtraLiftPower = SingletonObject.Get.getInputManager().ExtraLiftPower;
		//mExtraLiftRepetition = SingletonObject.Get.getInputManager().ExtraLiftRepetition;
	}

	void RegisterButtons() {
//		
//		inputManager.RegisterLeftClickDown(OnMouseDown);
//		inputManager.RegisterLeftClickUp(OnMouseUp);

		SingletonObject.Get.getInputManager().RegisterOnKeyDown (OnKeyDown);
	}

	void DeRegisterButtons() {
//		inputManager.DeregisterLeftClickUp(OnMouseUp);
//		inputManager.DeregisterLeftClickDown(OnMouseDown);

		SingletonObject.Get.getInputManager().DeregisterOnKeydown (OnKeyDown);
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}


	private void OnKeyDown(KeyCode key) {
		if (pScript.IsGrounded == false)
						return;

		if (pScript.PlayerNumber == 1) {
			// Jump
			InputManager inputManager = SingletonObject.Get.getInputManager();
			if (key == inputManager.key_p1_jump) {
				Jump();
				return;
			}
			// Tilt left
			if (key == inputManager.key_p1_tilt[2]) {
				AddForceInDirection(-Vector3.right);
				return;
			}
			// Tilt right
			if (key == inputManager.key_p1_tilt[3]) {
				AddForceInDirection(Vector3.right);
				return;
			}
			return;
		}
		if (pScript.PlayerNumber == 2) {
			InputManager inputManager = SingletonObject.Get.getInputManager();
			// Jump
			if (key == inputManager.key_p2_jump) {
				Jump();
				return;
			}

			// Tilt left
			if (key == inputManager.key_p2_tilt[2]) {
				AddForceInDirection(-Vector3.right);
				return;
			}
			// Tilt right
			if (key == inputManager.key_p2_tilt[3]) {
				AddForceInDirection(Vector3.right);
				return;
			}
			return;
		}

	}

	private void AddForceInDirection(Vector3 direction) {
		rigidbody.AddForceAtPosition(mUserForce * direction, mRealignForcePos.position);
	}

	private void Jump() {
		if (!pScript.IsGrounded) return; // Must be grounded to jump.
		// Jump in the direction of the up vector.
		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		allowJump = false;
		SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",canJump,0.1f,false, 0, null);
	}

	private void canJump() {
		allowJump = true;
	}
}












