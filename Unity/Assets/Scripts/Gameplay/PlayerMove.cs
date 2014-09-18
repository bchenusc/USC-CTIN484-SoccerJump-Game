using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]

public class PlayerMove : MonoBehaviour {
	string mID;

	// Globals
	private Timer timerManager;
	private InputManager inputManager;

	// Value will be set through InputManager.
	private float mMinJumpPower = 0;
	private float mExtraLiftPower = 0;
	private int mExtraLiftRepetition = 0;

	// Player movement gameplay
	private bool mIsGrounded = true;
	public bool IsGrounded { get { return mIsGrounded; } set { mIsGrounded = value;}}

	// Use this for initialization
	void Start () {
		mID = gameObject.GetInstanceID().ToString();
		SingletonObject singleton = SingletonObject.Get;
		inputManager = singleton.getInputManager();
		timerManager = singleton.getTimer();
		// Registers
		inputManager.RegisterLeftClickDown(OnMouseDown);
		inputManager.RegisterLeftClickUp(OnMouseUp);
		//inputManager.RegisterLeftClickUp(Remove
		// Variable initialization.
		mMinJumpPower = inputManager.MinJumpPower;
		mExtraLiftPower = inputManager.ExtraLiftPower;
		mExtraLiftRepetition = inputManager.ExtraLiftRepetition;
	}

	void OnDestroy() {
		inputManager.DeregisterLeftClickUp(OnMouseUp);
		inputManager.DeregisterLeftClickDown(OnMouseDown);
	}

	private void OnMouseDown(int whichMouseButton){
		if (whichMouseButton == 0) { Jump(); } // If Left mouse button, then jump.
	}

	private void OnMouseUp(int whichMouseButton) {
		if (whichMouseButton == 0) { RemoveExtraJumpLift(); } // If Left mouse button up, then no jump;
	}

	private void Jump() {
		if (!IsGrounded) return; // Must be grounded to jump.
		// Jump in the direction of the up vector.
		rigidbody.AddForce(transform.up * mMinJumpPower, ForceMode.Impulse);
		timerManager.Add(mID + "extrajumplift",
		                 ExtraJumpLift, 0.1f, true, mExtraLiftRepetition, null);
	}

	private void ExtraJumpLift() {
		Debug.Log("repeat");
		rigidbody.AddForce(transform.up * mExtraLiftPower);
	}

	private void RemoveExtraJumpLift() {
		timerManager.Remove(mID + "extrajumplift");
	}
}












