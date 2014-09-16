using UnityEngine;
using System.Collections;

public class test_Global_InputManager : MonoBehaviour {

	SingletonObject testSingletonObject;
	InputManager testInputManager;

	// Use this for initialization
	void Start () {
		// Test Singleton caching
		testSingletonObject = SingletonObject.Get;

		// Test Registering with input manager
		testInputManager = testSingletonObject.getInputManager();

		testInputManager.RegisterKeyCode(KeyCode.Space, true, true, true);
		testInputManager.RegisterOnKeyDown(DebugKeyPressed);
		testInputManager.RegisterOnKeyDown(DebugKeyPressed);
		testInputManager.RegisterOnKeyDown(DebugKeyPressed);

	}
	
	void DebugKeyPressed(KeyCode key) {
		Debug.Log ("Pressed " + key.ToString());
	}

	void DebugKeyUp(KeyCode key) {
		Debug.Log ("Up " + key.ToString());
	}

	void DebugKeyHeld(KeyCode key) {
		Debug.Log ("Held" + key.ToString());
	}
}







































