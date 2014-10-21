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
		SOCCER_GAME = 3,
		REPLAY = 4,
		OPTIONS = 5
	}
	public GAMESTATE mGameState = GAMESTATE.MAINMENU;

	private GameMode mMode;
	public GameMode MODE { get { return mMode; } }
	public SoccerMode GET_MODE_AS_SOCCER { 
		get { 
			if (mGameState == GAMESTATE.SOCCER_GAME)
				return (SoccerMode)mMode;
			return null;
		} 
	}

	private LinkedList<GameplayObject> startScripts = new LinkedList<GameplayObject> ();


#region MonoBehaviour functions
	void Start(){
		mMode = ModeFactory (mGameState);
		OnLevelWasLoaded (0);
	}

	public GameMode ModeFactory(GAMESTATE gs) {
		switch(gs) {
		case GAMESTATE.MAINMENU: return new MenuMode();
		case GAMESTATE.SOCCER_GAME: return new SoccerMode();
		default: return new MenuMode();
		}
	}

	public int LevelFactory(GAMESTATE gs) {
		switch(gs) {
		case GAMESTATE.MAINMENU: return 0;
		case GAMESTATE.SOCCER_GAME: return 1;
		default: return 0;
		}
	}

	void OnLevelWasLoaded(int i) {
		mMode.OnLevelLoaded ();
	}

	public void JumpToState(GAMESTATE gs) {
		mGameState = gs;
		mMode = ModeFactory (gs);
		Application.LoadLevel (LevelFactory (gs));
	}

	public void JumpToStateWPlayers(GAMESTATE gs, int controlledAvatars) {
		mGameState = gs;
		mMode = ModeFactory (gs);
		GET_MODE_AS_SOCCER.mControlledAvatars = controlledAvatars;
		Application.LoadLevel (LevelFactory (gs));
	}

	// Call this function to hard reset the game mode.
	public void ResetLevel() {
		mMode.ResetLevelDNU ();
		startScripts.Clear ();
		SingletonObject.Get.getTimer ().RemoveAll ();
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ResetLevelWithoutResettingScore() {
		//startScripts.Clear ();
		//SingletonObject.Get.getTimer ().RemoveAll ();
		Time.timeScale = 1.0f;
		//Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadGameMode() {
		mMode.LoadGameMode ();
		GameStartEntry();	
	}

	// Start functions
	public void GameStartEntry() {
		foreach (GameplayObject g in startScripts) {
			g.GameStart();
		}
	}

	public void RegisterScriptAsGameplayObject(GameplayObject go) {
		startScripts.AddLast (go);
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






