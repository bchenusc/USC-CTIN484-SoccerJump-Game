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

	public int mNumberOfPlayers = 0;

	private int mTeam1BlueScore = 0;
	private int mTeam2RedScore = 0;
	private Action UpdateScoreLabels;

	// HACK USE PUSH SYSTEM
	private GUIText mStartTimerGUI;
	private int mTimeTillStart = 5;

	public int Team1BlueScore { get { return mTeam1BlueScore; } }
	public int Team2RedScore { get { return mTeam2RedScore; } }

	private LinkedList<GameplayObject> startScripts = new LinkedList<GameplayObject> ();


#region MonoBehaviour functions
	void Start(){
		OnLevelWasLoaded (0);
	}

	void OnLevelWasLoaded(int i) {
		Time.timeScale = 1;
		mTimeTillStart = 3;
		mStartTimerGUI = GameObject.Find ("StartTimer").transform.GetComponent<GUIText> ();
		mStartTimerGUI.text = "3";
		
		SingletonObject.Get.getTimer ().Add ("GameModeStart", GuiStartCounter, 1.0f, false, mTimeTillStart, LoadGameMode);
	}

	// Call this function to reset the game mode.
	public void ResetLevel(int level) {
		startScripts.Clear ();
		mTeam1BlueScore = 0;
		mTeam2RedScore = 0;

		Application.LoadLevel (level);
	}

	public void RoundOver(int teamWhoWon) {
		Time.timeScale = 0.3f;

		if (teamWhoWon == 1) {
			// Blue won.
			mStartTimerGUI.text = "BLUE SCORED!";
		} else if (teamWhoWon == 2) {
			mStartTimerGUI.text = "RED SCORED!";
		}
		mStartTimerGUI.enabled = true;

		SingletonObject.Get.getTimer ().Add ("newRound", ResetLevelWithoutResettingScore, 1.0f, false);

	}

	private void ResetLevelWithoutResettingScore() {
		startScripts.Clear ();
		SingletonObject.Get.getTimer ().RemoveAll ();
		Application.LoadLevel (Application.loadedLevel);
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

	void GuiStartCounter() {
		mTimeTillStart --;
		mStartTimerGUI.text = mTimeTillStart.ToString ();
	}

	void LoadGameMode() {
		mStartTimerGUI.enabled = false;
		GameStartEntry();
	}
#endregion

	#region GamePlay
	public void AddScore(int team) {
		if (team == 1) {
			mTeam1BlueScore ++;
		} else if (team == 2) {
			mTeam2RedScore ++;
		}

		// Update the score boards
		if (UpdateScoreLabels != null) {
			Debug.Log ("Updating");
			UpdateScoreLabels ();
		}

		RoundOver (team);

	}

	public void RegisterScoreLabel(Action a) {
		UpdateScoreLabels += a;
	}
	public void DeRegisterScoreLabel(Action a) {
		UpdateScoreLabels -= a;
	}

	#endregion


	// Use settings menu to control the controls.
	public void SetNumberOfPlayers(int players) {

	}


#region Inherited functions
	protected override void DestroyIfMoreThanOneOnObject(){
		if (transform.GetComponents<GameState>().Length > 1){
			Debug.Log ("Destroying Extra " + this.GetType() + " Attachment");
			DestroyImmediate(this);
		}
	}
#endregion
}






