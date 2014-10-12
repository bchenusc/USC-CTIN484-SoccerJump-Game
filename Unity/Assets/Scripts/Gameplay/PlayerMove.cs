using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : GameplayObject {

	// Dependencies
	private Transform mRealignForcePos;
	private PlayerScript pScript;

	// Value will be set through InputManager.
	private float mMinJumpPower = 850;

	// User control force
	private float mUserForce = 1000;

	void Start() {
		pScript = gameObject.GetComponent<PlayerScript> ();
		SingletonObject.Get.getGameState ().RegisterScriptAsGameplayObject (this);
		mRealignForcePos = transform.FindChild ("UpPoint");
	}

	// Use this for initialization
	public override void GameStart () {
		RegisterKeys ();
		RegisterButtons ();
	}

	void RegisterButtons() {
		SingletonObject.Get.getInputManager().RegisterOnKeyHeld (OnKeyHeld);
		SingletonObject.Get.getInputManager().RegisterOnKeyDown (OnKeyDown);
	}

	void RegisterKeys() {
		InputManager iManager = SingletonObject.Get.getInputManager ();
		PlayerConfig player = iManager.mPlayers [pScript.PlayerNumber - 1];
		iManager.RegisterKeyCode(player.Jump, true, false, false);
		iManager.RegisterKeyCode(player.Left, false, false, true);
		iManager.RegisterKeyCode(player.Right, false, false, true);
	}

	void DeRegisterButtons() {
		SingletonObject instance = SingletonObject.Get;
		if (instance == null) {
			return;
		}
		 InputManager iManager = instance.getInputManager ();
		 iManager.DeRegisterAllKeyCodes ();
		 iManager.DeregisterOnKeyHeld (OnKeyHeld);
		 iManager.DeregisterOnKeyDown (OnKeyDown);
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}


	private void OnKeyDown(KeyCode key) {
		if (pScript.IsGrounded == false)
			return;

		if (CONTROLS.DBGKEY && CONTROLS.DBG) { Debug.Log ("PlayerMove - OnKeyDown: " + key.ToString()); }
		// Maybe cache in player
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber-1];
		if (key == pConfig.Jump) {
			Jump();
			return;
		}
	}


	private void OnKeyHeld(KeyCode key) {
		if (CONTROLS.DBGKEY && CONTROLS.DBG) { Debug.Log ("PlayerMove - OnKeyHeld: " + key.ToString()); }
		PlayerConfig pConfig = SingletonObject.Get.getInputManager().mPlayers[pScript.PlayerNumber-1];
		if (key == pConfig.Right) {
			AddForceInDirection(Vector3.right);
			AddMomentumForce(Vector3.right);
			return;
		}
		if (key == pConfig.Left) {
			AddForceInDirection(Vector3.left);
			AddMomentumForce(Vector3.left);
			return;
		}
	}

	private void AddForceInDirection(Vector3 direction) {
		if (!pScript.IsGrounded) {
			// Allows aerial flipping
			rigidbody.AddTorque (-Mathf.Sign(direction.x) * Vector3.forward * 1000);
		} else {
			// Does not allow rolling on ground.
			if (transform.up.y > 0) {
				rigidbody.AddRelativeTorque (Mathf.Sign(direction.x) * Vector3.Dot(Vector3.up, transform.up) * Vector3.forward * 1000);
			}
		}
	}

	private void AddMomentumForce(Vector3 direction) {
		// Momentum force only added in the air to make flipping feel a little more polished.
		if (!pScript.IsGrounded) {
			rigidbody.AddForceAtPosition (direction * 200, transform.position);
		}
	}

	private void Jump() {
		if (!pScript.IsGrounded) return; // Must be grounded to jump.
		// Jump in the direction of the up vector.
		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		SingletonObject.Get.getSoundManager().play("jump");
		SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",null,0.1f,false, 0, null);
	}

}












