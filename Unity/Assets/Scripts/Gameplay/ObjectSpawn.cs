using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawn : MonoBehaviour {

	List<GameObject> SpawnList;
	int objCount = 0;

	void FillList()
	{
		SpawnList.Add(Resources.Load("DropPrefabs/DropObject") as GameObject);
		SpawnList.Add(Resources.Load("DropPrefabs/DropSphere") as GameObject);
		//SpawnList.Add(Resources.Load("DropPrefabs/DropGrassBlock") as GameObject);
	}

	void Start() {
		SpawnList = new List<GameObject>();
		SingletonObject.Get.getTimer ().Add ("objectSpawner", createObject, 10f, true);
	}

	void createObject() {
		if (SpawnList.Count == 0) FillList();
		if (objCount < 5)
		{
			int type = Random.Range(0, SpawnList.Count);
			float x, y;
			x = Random.Range (-1000, 1000) / 100f; // random horizontal placement
			y = 15f;
			Vector3 coord = new Vector3(x, y, 0);
			GameObject obj = Instantiate(SpawnList[type], coord, Quaternion.identity) as GameObject;
			obj.transform.parent = transform;
			obj.AddComponent<ObjDestructor>();
            obj.GetComponent<ObjDestructor>().setSpawner(this);
            objCount++;
		}
	}
    
    public void decreaseCount()
    {
        objCount--;
    }
}







