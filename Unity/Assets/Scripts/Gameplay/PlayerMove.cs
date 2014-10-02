using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : GameplayObject {
	// Dependencies
	Transform mRealignForcePos;
	PlayerScript pScript;

	// Value will be set through InputManager.
	private float mMinJumpPower = 0;

	// User control force
	private float mUserForce = 900;

	void Start() {
		SingletonObject.Get.getGameState ().RegisterScriptAsGameplayObject (this);
		pScript = gameObject.GetComponent<PlayerScript> ();
		mRealignForcePos = transform.FindChild ("UpPoint");
	}

	// Use this for initialization
	public override void GameStart () {
		RegisterKeys ();
		RegisterButtons ();
		// Variable initialization.
		mMinJumpPower = SingletonObject.Get.getInputManager().MinJumpPower;
	}

	void RegisterButtons() {
		SingletonObject.Get.getInputManager().RegisterOnKeyHeld (OnKeyHeld);
		SingletonObject.Get.getInputManager().RegisterOnKeyDown (OnKeyDown);
	}

	void RegisterKeys() {
		InputManager iManager = SingletonObject.Get.getInputManager ();
		PlayerConfig player = iManager.mPlayers [pScript.PlayerNumber - 1];
		// Determines whether keys are registered for Key up, key down, or key held.
		iManager.RegisterKeyCode(player.Jump, true, false, false);
		iManager.RegisterKeyCode(player.Left, false, false, true);
		iManager.RegisterKeyCode(player.Right, false, false, true);
		if (CONTROLS.DBG) { iManager.dbg_PrintEnabledKeys();}
	}

	void DeRegisterButtons() {
		// InputManager iManager = SingletonObject.Get.getInputManager ();
		// iManager.DeRegisterAllKeyCodes ();
		// iManager.DeregisterOnKeyHeld (OnKeyHeld);
		// iManager.DeregisterOnKeyDown (OnKeyDown);
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}

	private void OnKeyDown(KeyCode key) {
		if (CONTROLS.DBG) { Debug.Log ("PlayerMove - OnKeydown: Pressed " + key.ToString()); }
		if (pScript.IsGrounded == false)
			return;
		// Maybe cache in player
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber-1];
		if (key == pConfig.Jump) {
			Jump();
			return;
		}
	}


	private void OnKeyHeld(KeyCode key) {
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber-1];
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
		rigidbody.AddForceAtPosition(Vector3.Dot(Vector3.up, transform.up) * mUserForce * direction, mRealignForcePos.position);
	}

	private void Jump() {
		if (!pScript.IsGrounded) return; // Must be grounded to jump.
		// Jump in the direction of the up vector.
		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",null,0.1f,false, 0, null);
	}

}












