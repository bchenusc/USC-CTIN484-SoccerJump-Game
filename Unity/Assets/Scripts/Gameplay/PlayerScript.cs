using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	private int mPlayerNumber = 1;
	public int mPlayableID = 0;
	public int PlayerNumber { get { return mPlayerNumber; } set { mPlayerNumber = value; } }
	
	private Vector3 initCenterOfMass;

	private event Action CollisionExit;

	// Player movement gameplay
	private bool mIsGrounded = false;
	public bool IsGrounded { get { return mIsGrounded; } set { mIsGrounded = value;}}

	bool PlayThudSound = false;

	void Awake() {
		initCenterOfMass = rigidbody.centerOfMass;
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
			PlayThudSound = true;
			rigidbody.isKinematic = true;
			transform.position = transform.GetComponent<PlayerGameState>().mSoccerGamePosition + Vector3.up * 15;
			SingletonObject.Get.getTimer().Add(gameObject.GetInstanceID() + "respawning", RespawnMe, 2.0f, false);
			return;
		} else 
		{
			if (PlayThudSound)
			{
				PlayThudSound = false;
				Vector3 effectLocation = transform.position;
				effectLocation.Set (transform.position.x + 0.5f, 0.5f, transform.position.z);
				GameObject clone = Instantiate(Resources.Load("Effects/Thud", typeof(GameObject)), 
				                               effectLocation , 
				                               Quaternion.AngleAxis(-90, Vector3.right)) as GameObject;
				Destroy (clone, 1.0f);
				SingletonObject.Get.getSoundManager().play ("Audio/whoop_big_thud", false, 2);
			}
		}
	}

	void RespawnMe() {
		if (this == null) return;
		rigidbody.isKinematic = false;
		rigidbody.AddForce(Vector3.down);
	}

	void OnCollisionStay(Collision c) {
		if (!c.gameObject.CompareTag("Ball"))
		mIsGrounded = true;
	}

	void OnCollisionExit(Collision c) {
		mIsGrounded = false;
		rigidbody.centerOfMass = initCenterOfMass;
		if (CollisionExit != null) {
			CollisionExit();
		}
		if (c.gameObject.CompareTag("Ball")) {
			if (mPlayerNumber == 1 || mPlayerNumber == 3) {
				SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.UpdateLastTouch(2);
			}
			else {
				SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.UpdateLastTouch(1);
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