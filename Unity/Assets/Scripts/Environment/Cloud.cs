using UnityEngine;

public class Cloud : MonoBehaviour {

	float mSpeed;

	public float Speed { get { return mSpeed; } set { mSpeed = value; } }

	void Start () {
		mSpeed = Random.Range (2, 10) / 100f;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * mSpeed);
		if (transform.position.x > 50) Destroy (this.gameObject);
	}
}
