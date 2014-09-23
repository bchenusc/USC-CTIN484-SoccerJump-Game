using UnityEngine;
using System.Collections;

public class BallSpike : MonoBehaviour {

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.CompareTag("SpikeBall")) {
			//rigidbody.AddForce((transform.position - c.transform.position) * 50, ForceMode.Impulse);
		}
	}
}
