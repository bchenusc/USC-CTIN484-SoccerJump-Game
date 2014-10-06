﻿using UnityEngine;
using System.Collections.Generic;

public class CloudSpawn : Singleton {

	bool timer;
	Timer cloudTimer;
	public Transform Cloud1;
	public Transform Cloud2;
	public Transform Cloud3;

	void Start() {
		timer = false;
		for (int i = 0; i < 5; i++) {
			createCloud(false);
		}
	}

	void Update () {
		if (! timer)
		{
			timer = true;
			SingletonObject.Get.getTimer().Add("cloudTimer", createCloud, Random.Range (25, 65) / 10f, false, 0, null);
		}
	}
	
	void createCloud(bool fixedX = true) {
		int type = Random.Range (1, 7);
		float x, y;
		if (fixedX) x = -50f; // fixed horizontal placement off left side of screen (for spawned clouds)
		else x = Random.Range (-5000, 3000) / 100f; // random horizontal placement (for initial clouds)
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
		cloud.parent = GameObject.Find ("Sky").transform;
		timer = false;
	}
	
	

	#region Inherited functions
	protected override void DestroyIfMoreThanOneOnObject(){
		if (transform.GetComponents<InputManager>().Length > 1){
			Debug.Log ("Destroying Extra " + this.GetType() + " Attachment");
			DestroyImmediate(this);
		}
	}
	#endregion

}







