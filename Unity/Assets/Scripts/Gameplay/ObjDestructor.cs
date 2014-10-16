using UnityEngine;
using System.Collections;

public class ObjDestructor : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Deadzone")) {
			Destroy (gameObject);
		}
	}
}