using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawn : MonoBehaviour {

	public  List<GameObject> SpawnList;
	private List<GameObject> ObjList;

	void FillList()
	{
		SpawnList.Add(Resources.Load("DropPrefabs/DropObject") as GameObject);
		SpawnList.Add(Resources.Load("DropPrefabs/DropGrassBlock") as GameObject);
	}

	void Start() {
		SpawnList = new List<GameObject>();
		ObjList = new List<GameObject>();
		SingletonObject.Get.getTimer ().Add ("objectSpawner", createObject, 10f, true);
	}

	void createObject() {
		if (SpawnList.Count == 0) FillList();
		if (ObjList.Count < 5)
		{
			int type = Random.Range(0, SpawnList.Count);
			float x, y;
			x = Random.Range (-1000, 1000) / 100f; // random horizontal placement
			y = 15f;
			Vector3 coord = new Vector3(x, y, 0);
			GameObject obj = Instantiate(SpawnList[type], coord, Quaternion.identity) as GameObject;
			ObjList.Add(obj);
		}
		else
		{
			foreach (GameObject obj in ObjList)
			{
				if (obj == null) ObjList.Remove(obj);
			}
		}
	}
}







