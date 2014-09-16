using UnityEngine;
using System.Collections.Generic;

/*
 * How to use:
 * 1. Place on a Game object in the scene that you want to be persistant throughout all levels.
 * 
 * @ Brian Chen
*/


public class GameState : Singleton {

	//Game Logic Here


	//Game Progression
	public List<string> SCENES;
	private int i_CurrentLevel = 0;
	private int i_NextLevelQueue = 0; //set this before fading out to change levels.
	
#region MonoBehaviour functions
	new void Start(){
		base.Start();
		OnLevelWasLoaded(0); //This is called when the first time the GameState is initialized.
			
	}

	void Update(){

	}

	void OnLevelWasLoaded(int i){
		i_CurrentLevel = GetCurrentLevelInt();
		i_NextLevelQueue = 0; // I set this to zero here. When you change levels, make sure you set which level you are changing to.

	}
#endregion
	

#region Level Getters Functions
	/* @param Current level
	 * @return String name of the level.
	 * */
	public string GetLevelName(int currentLevel){
		return SCENES[currentLevel];
	}

	/* @param Current level
	 * @return String name of the next level.
	 * */
	public string GetNextLevelString(int currentLevel){
		//if level is not the last level.
		return SCENES[GetNextLevelInt(currentLevel)];
	}

	/* @param Current level
	 * @return index in SCENES of the next level or 0 if current level is last index.
	 * */
	public int GetNextLevelInt(int currentLevel){
		//if level is not the last level.
		if (0<=currentLevel && currentLevel < SCENES.Count-1){
			return currentLevel+1;
		}else{
			Debug.Log ("GetNextLevelInt(" + currentLevel + ") last level. Returning Level 0");
			return 0;
		}
	}

	/* @param Name of a level in SCENES
	 * @return index in SCENES of the param level or 0 if level not found.
	 * */
	public int GetLevelInt(string nameOfALevel){
		// O log(n)
		for (int i=0; i< SCENES.Count; i++){
			if (SCENES[i].Equals(nameOfALevel)){
				return i;
			}
		}
		Debug.Log ("GetLevelInt(" + nameOfALevel + ") not found. Returning Level 0");
		return 0;
	}

	/* 
	 * @return index in SCENES of the current level or 0 if somehow not found.
	 * */
	public int GetCurrentLevelInt(){
		// O log(n)
		for (int i=0; i< SCENES.Count; i++){
			if (SCENES[i].Equals(Application.loadedLevelName)){
				return i;
			}
		}
		Debug.Log ("CurrentLevelInt() not found. Returning Level 0");
		return 0;
	}


	//Loads next level in SCENES loads level 0 if no level found.
	public void LoadNextLevelQueued(){
		i_NextLevelQueue = GetNextLevelInt(i_CurrentLevel);
		Application.LoadLevel(GetNextLevelString(i_NextLevelQueue));
	}

	//Loads "level to load" which is an index of a level in SCENES.
	public void LoadLevel(int levelToLoad){
		i_NextLevelQueue = levelToLoad;
		Application.LoadLevel(i_NextLevelQueue);
	}

	//Loads a level based on the name of the level.
	public void LoadLevel(string levelToLoadName){
		i_NextLevelQueue = GetLevelInt(levelToLoadName);
		Application.LoadLevel(i_NextLevelQueue);
	}

	//Reloads the current level.
	public void LoadCurrentLevel(){
		i_NextLevelQueue = i_CurrentLevel;
		Application.LoadLevel(i_NextLevelQueue);
	}
#endregion


#region Inherited functions
	protected override void DestroyIfMoreThanOneOnObject(){
		if (transform.GetComponents<GameState>().Length > 1){
			Debug.Log ("Destroying Extra " + this.GetType() + " Attachment");
			DestroyImmediate(this);
		}
	}
#endregion
}






