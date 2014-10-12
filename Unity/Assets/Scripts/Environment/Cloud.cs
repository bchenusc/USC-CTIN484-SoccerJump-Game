using UnityEngine;

public class Cloud : MonoBehaviour {

	float mSpeed;

	public float Speed { get { return mSpeed; } set { mSpeed = value; } }

	void Start () {
		mSpeed = Random.Range (2, 10)/3f;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * mSpeed * Time.deltaTime);
		if (transform.position.x > 50) {
			int loc = Random.Range(0, 2);
			float y;
			if (loc == 0) y = Random.Range (-4000, -2500) / 100f; // random vertical placement in lower area
			else y = (float) Random.Range (-250, 1500) / 100f; // random vertical placement in upper area
			Vector3 wrapPos = new Vector3(-50, y, transform.position.z);
			transform.position = wrapPos;
			mSpeed = Random.Range (2, 10)/3f;
		}
	}
}
