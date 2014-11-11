using UnityEngine;
using System.Collections;

public class CloudPlatform : MonoBehaviour {

	public int direction = 0;
	bool destroying = false;

	void Update()
	{
		if (destroying)
						return;
		if (Mathf.Abs(transform.position.x) > 30)
		{
			destroying = true;
			Destroy (gameObject);

		}

		if (direction == 0)
						direction = 1;
		direction = direction / Mathf.Abs (direction);
		transform.Translate (Time.deltaTime * Vector3.right * direction * Random.Range (3, 10) / 3.0f);
	}
}
