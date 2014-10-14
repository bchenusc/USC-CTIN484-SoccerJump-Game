using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private int mPlayerNumber = 1;
	public int PlayerNumber { get { return mPlayerNumber; } set { mPlayerNumber = value; } }

	private Vector3 spawnPosition;

	private event Action CollisionExit;

	// Player movement gameplay
	private bool mIsGrounded = false;
	public bool IsGrounded { get { return mIsGrounded; } set { mIsGrounded = value;}}

	void Awake() {
		spawnPosition = transform.position;
		// HACK -- Make sure this doesn't break anything in the future.
		if (transform.name.Contains("1")) {
			mPlayerNumber = 1;
		} else if (transform.name.Contains("2")) {
			mPlayerNumber = 2;
		} else if (transform.name.Contains("3")) {
			mPlayerNumber = 3;
		} else if (transform.name.Contains("4")) {
			mPlayerNumber = 4;
		}
	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.CompareTag("Deadzone")) {
			rigidbody.isKinematic = true;
			transform.position = spawnPosition + Vector3.up * 15;
			SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "respawning", RespawnMe, 3.0f, false);
			return;
		}
		if (c.gameObject.CompareTag("Ball")) {
			if (mPlayerNumber == 1 || mPlayerNumber == 3) {
				c.transform.renderer.material.color = Color.red;
			} else {
				c.transform.renderer.material.color = Color.blue;
			}
		}
	}

	void RespawnMe() {
		rigidbody.isKinematic = false;
		rigidbody.AddForce(Vector3.down);
	}

	void OnCollisionStay(Collision c) {
		if (!c.gameObject.CompareTag("Ball"))
		mIsGrounded = true;
	}

	void OnCollisionExit(Collision c) {
		mIsGrounded = false;
		if (CollisionExit != null) {
			CollisionExit();
		}
		if (c.gameObject.CompareTag("Ball")) {
			if (mPlayerNumber == 1 || mPlayerNumber == 3) {
				SingletonObject.Get.getGameState().UpdateLastTouch(2);
			}
			else {
				SingletonObject.Get.getGameState().UpdateLastTouch(1);
			}

		}
	}

	public void RegisterCollisionExit(Action func) {
		CollisionExit += func;
	}

	public void DeRegisterCollisionExit(Action func) {
		CollisionExit -= func;
	}

}