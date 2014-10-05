using UnityEngine;

public class Cloud : MonoBehaviour {

	float mSpeed = 0;

	public float Speed { get { return mSpeed; } set { mSpeed = value; } }

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * mSpeed);
	}
}
