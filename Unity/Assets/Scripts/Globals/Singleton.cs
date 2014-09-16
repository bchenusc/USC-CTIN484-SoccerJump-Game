using UnityEngine;
using System.Collections;

/* How to use:
 * 1. This is only a base class. It cannot be instantiated.
 * 2. Derive any singletons you want to place on your singleton transform from this class.
 * 
 * @Brian Chen
 */

public abstract class Singleton : MonoBehaviour {
	//Do not override this!!!
	protected void Awake(){
		DestroyIfNotSingletonObject();
		DestroyIfMoreThanOneOnObject();
	}

	protected void Start(){
	}

	private void DestroyIfNotSingletonObject(){
		//Singletons can only be placed on singleton objects.
		if (transform.GetComponent<SingletonObject>() == null){
			DestroyImmediate(this);
		}
	}

	protected abstract void DestroyIfMoreThanOneOnObject();

}










