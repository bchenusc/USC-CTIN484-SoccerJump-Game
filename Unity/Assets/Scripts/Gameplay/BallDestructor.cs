using UnityEngine;
using System.Collections;

public class BallDestructor : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Deadzone")) {
			rigidbody.velocity = Vector3.zero;
			transform.position = Vector3.up * 20;
			rigidbody.AddForce(Vector3.down);
		}
	}
}
