using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawn : MonoBehaviour {

	public Transform Obj;

	void Start() {
		SingletonObject.Get.getTimer ().Add ("objectSpawner", createObject, 10f, true);
	}

	void createObject() {
		int type = Random.Range (1, 4);
		float x, y;
		x = Random.Range (-1000, 1000) / 100f; // random horizontal placement
		y = 15f;
		Vector3 coord = new Vector3(x, y, 0);
		Transform obj = Instantiate(Obj, coord, Quaternion.identity) as Transform;
	}
}







