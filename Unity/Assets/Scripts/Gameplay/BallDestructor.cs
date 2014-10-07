using UnityEngine;
using System.Collections;

public class BallDestructor : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Deadzone")) {
			Destroy (gameObject);
		}
	}
}
