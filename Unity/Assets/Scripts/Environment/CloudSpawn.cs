using UnityEngine;
using System.Collections;

public class CloudSpawn : MonoBehaviour {
	public Transform Cloud1;
	public Transform Cloud2;
	public Transform Cloud3;

	void Start() {
		for (int i = 0; i < 5; i++) {
			createCloud();
		}
	}

	void createCloud() {
		int type = Random.Range (1, 7);
		float x, y;
		x = Random.Range (-5000, 3000) / 100f; // random horizontal placement (for initial clouds)
		if (type > 3) y = Random.Range (-4000, -2500) / 100f; // random vertical placement in lower area
		else y = (float) Random.Range (-250, 1500) / 100f; // random vertical placement in upper area
		Vector3 coord = new Vector3(x, y, 30);
		Transform cloud;
		switch (type) {
		case 1:
		case 4:  cloud = Instantiate(Cloud1, coord, Quaternion.identity) as Transform; break;
		case 2:
		case 5:  cloud = Instantiate(Cloud2, coord, Quaternion.identity) as Transform; break;
		case 3:
		case 6:  cloud = Instantiate(Cloud3, coord, Quaternion.identity) as Transform; break;
		default: cloud = Instantiate(Cloud1, coord, Quaternion.identity) as Transform; break;
		}
		cloud.parent = GameObject.Find("Sky").transform;
	}
}







