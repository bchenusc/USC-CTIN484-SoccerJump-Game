using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : GameplayObject {

	// Dependencies
	private PlayerScript pScript;

	// Value will be set through InputManager.
	private float mMinJumpPower = 800;
	private float mTiltPower = 2100;

	bool canPlayFallingSound = false;

	void Start() {
		rigidbody.isKinematic = false;
		pScript = gameObject.GetComponent<PlayerScript> ();
		SingletonObject.Get.getGameState ().RegisterScriptAsGameplayObject (this);
		GameStart ();
	}
	
	void OnLevelWasLoaded(int i) {
		if (i == 1) {
			// Check if the game state is game mode.
			if (SingletonObject.Get.getGameState().mGameState == GameState.GAMESTATE.SOCCER_GAME) {
				rigidbody.isKinematic = true;
			}
		}
	}
	
	// Use this for initialization
	public override void GameStart () {
		RegisterKeys ();
		RegisterButtons ();
		if (this == null) return;
		rigidbody.isKinematic = false;
		canPlayFallingSound = true;
		rigidbody.AddForce (Vector3.down * 10);
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

	void Update()
	{
		if (canPlayFallingSound && transform.position.y < -0.1)
		{
			SingletonObject.Get.getSoundManager().play("Audio/fall_sound_1");
			canPlayFallingSound = false;
		} 
		if (transform.position.y > 10)
		{
			canPlayFallingSound = true;
		}
	}

	void OnDestroy() {
		DeRegisterButtons ();
	}


	private void OnKeyDown(KeyCode key) {
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
		if (this == null)
			return;
			rigidbody.AddTorque (-Mathf.Sign(direction.x) * Vector3.forward * mTiltPower);
	}

	private void AddMomentumForce(Vector3 direction) {
		if (this == null) return;
		// Momentum force only added in the air to make flipping feel a little more polished.
		if (!pScript.IsGrounded) {
			rigidbody.AddForceAtPosition (direction * 200 * Time.deltaTime, transform.position);
		}
	}

	private void Jump() {
		if (this == null) return;
		if (!pScript.IsGrounded) return; // Must be grounded to jump.

		AddToMetrics();

		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		int soundNum = Random.Range(1,7);
		SingletonObject.Get.getSoundManager().play("Audio/jump_" + soundNum, false, 5f);
		//SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "jump",null,0.1f,false, 0, null);
	}
	
	private void AddToMetrics() {
		if (this.gameObject.name.Equals("Player1")) SingletonObject.Get.getMetricManager().AddToRed();
		else SingletonObject.Get.getMetricManager().AddToBlue();
	}

}












