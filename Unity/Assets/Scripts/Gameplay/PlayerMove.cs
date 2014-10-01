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
		SingletonObject.Get.getInputManager().RegisterOnKeyDown (OnKeyDown);
	}

	void RegisterKeys() {
		InputManager iManager = SingletonObject.Get.getInputManager();
		iManager.mPlayers[
		iManager.DeRegisterAllKeyCodes();
		iManager.RegisterKeyCode(
	}

	void DeRegisterButtons() {
		SingletonObject.Get.getInputManager().DeregisterOnKeyHeld (OnKeyHeld);
		SingletonObject.Get.getInputManager().DeregisterOnKeyDown (OnKeyDown);
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}

	private void OnKeyDown(KeyCode key) {
		if (CONTROLS.DBG) { Debug.Log ("PlayerMove - OnKeydown: Pressed"); }
		if (pScript.IsGrounded == false)
			return;
		// Maybe cache in player
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber];
		if (key == pConfig.Jump) {
			Jump();
			return;
		}
	}


	private void OnKeyHeld(KeyCode key) {
		if (CONTROLS.DBG) { Debug.Log ("PlayerMove - OnKeyheld: Pressed"); }
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber];
		if (key == pConfig.Right) {
			AddForceInDirection(Vector3.right);
			return;
		}
		if (key == pConfig.Left) {
			AddForceInDirection(Vector3.left);
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
		SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",null,0.1f,false, 0, null);
	}

}












