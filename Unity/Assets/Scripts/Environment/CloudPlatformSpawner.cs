using UnityEngine;
using System.Collections;

public class CloudPlatformSpawner : MonoBehaviour {

	uint cloudTracker = 0;
	float minTime = 20.0f;
	float maxTime = 60.0f;
	
	public Transform[] cloudPrefabs = null;
	
	// Use this for initialization
	void Start () {
		SpawnCloudPlatform ();
		SingletonObject.Get.getTimer ().Add (gameObject.GetInstanceID () + cloudTracker + "", SpawnCloudPlatform
		                                     , Random.Range (minTime, maxTime), false);
		
	}
	
	void SpawnCloudPlatform()
	{
		++cloudTracker;
		// Actually spawn a platform
		Vector3 spawnPosition = Vector3.zero;
		int direction = (Random.Range (0, 2) * 2 - 1);
		spawnPosition.Set (25 * direction, Random.Range (0, 12), 0);
		Transform clone = Instantiate(cloudPrefabs[Random.Range (0, cloudPrefabs.Length)],
		                                           spawnPosition, Quaternion.identity) as Transform;
		clone.GetComponent<CloudPlatform> ().direction = -direction;
		clone.tag = "Cloud";
		SingletonObject.Get.getTimer ().Add (gameObject.GetInstanceID () + cloudTracker+"", SpawnCloudPlatform
		                                     , Random.Range (minTime, maxTime), false);
	}

}
