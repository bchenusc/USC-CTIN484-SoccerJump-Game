using UnityEngine;
using System.Collections.Generic;

/*
 * How to use:
 * 1. Place on a Game object in the scene that you want to be persistant throughout all levels.
 * 2. There is only one scene. All menu and gameplay will be performed in one scene.
 * 
 * @ Brian Chen
*/


public class GameState : Singleton {
	
	public enum GAMESTATE {
		MAINMENU = 0,
		GAMESELECTION = 1,
		CHARACTERSELECTION = 2,
		GAMEPLAY = 3,
		REPLAY = 4,
		OPTIONS = 5
	}
	public GAMESTATE mGameState = GAMESTATE.MAINMENU;


#region MonoBehaviour functions
	void Start(){
		// This will ALWAYS start in the main menu.

	}

	void Update(){

	}

	void Transition() {

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






