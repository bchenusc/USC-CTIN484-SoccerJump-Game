using UnityEngine;
using System.Collections;

public class ObjDestructor : MonoBehaviour {

    ObjectSpawn spawner;

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Deadzone")) {
			
            spawner.decreaseCount();
			Destroy (gameObject);
		}
	}
    
    public void setSpawner(ObjectSpawn os)
    {
        spawner = os;
    }
}