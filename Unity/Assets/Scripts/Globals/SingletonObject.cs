using UnityEngine;
using System.Collections;

/* How to use:
 *  1. Place on a scene object to make it a singleton.
 * 		- That scene object will never be destroyed.
 * 		- Place all singleton classes on that scene object.
 * 	2. If any other Singleton type object is created, the other Singleton will be destroyed.
 * 
 * @Brian Chen
 */

public class SingletonObject : MonoBehaviour {
	private Timer m_Timer;
	private GameState m_GameState;
	private InputManager m_InputManager;
//	private GuiManager m_GuiManager;
	
	void Awake () {
		InitSingleton(); //Initialize singleton -- DO NOT TOUCH
	}
	
	public Timer getTimer(){
		return m_Timer;
	}
	public GameState getGameState() {
		return m_GameState;
	}
	public InputManager getInputManager() {
		return m_InputManager;
	}
//	public GuiManager getGuiManager(){
//		return m_GuiManager;
//	}
	
	
	#region Singleton Instantiation
	private static SingletonObject _instance;
	private int _AmSingleton = 0;
	private static object _lock = new object();
	
	private void InitSingleton(){
		
		if (FindObjectsOfType(typeof (SingletonObject)).Length > 1){
			if (_AmSingleton == 0){
				Debug.Log ("Already Instantiated SingletonObject");
				DestroyImmediate(gameObject);
				return;
			}
		}
		DontDestroyOnLoad(gameObject);
		_AmSingleton = 1;
		_instance = GameObject.FindGameObjectWithTag("SingletonObject").GetComponent<SingletonObject>();
		m_Timer = _instance.GetComponent<Timer>();
		m_GameState = _instance.GetComponent<GameState>();
		m_InputManager = _instance.GetComponent<InputManager>();
//		m_GuiManager = _instance.GetComponent<GuiManager>();
	}
	
	public static SingletonObject Get
	{
		get
		{
			if (_instance != null && _instance.applicationIsQuitting) {
				Debug.LogWarning("[Singleton] Instance '"+ typeof(SingletonObject) +
				                 "' already destroyed on application quit." +
				                 " Won't create again - returning null.");
				return null;
			}
			
			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = GameObject.FindGameObjectWithTag("SingletonObject").GetComponent<SingletonObject>();
					if (_instance == null) 
						return null;
					
					if ( FindObjectsOfType(typeof(SingletonObject)).Length > 1 )
					{
						Debug.LogError("[Singleton] Something went really wrong " +
						               " - there should never be more than 1 singleton!" +
						               " Reopenning the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<SingletonObject>();
						singleton.name = "(singleton) "+ typeof(SingletonObject).ToString();
						
						DontDestroyOnLoad(singleton);
						
						Debug.Log("[Singleton] An instance of " + typeof(SingletonObject) + 
						          " is needed in the scene, so '" + singleton +
						          "' was created with DontDestroyOnLoad.");
					} else {
						Debug.Log("[Singleton] Using instance already created: " +
						          _instance.gameObject.name);
					}
				}
				
				return _instance;
			}
		}
	}
	
	
	private bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy () {
		applicationIsQuitting = true;
	}
	
	#endregion
}
