using UnityEngine;
using System.Collections;

public class SoccerMode : GameMode {
	private const int MAX_GOALS = 5;

	public int mNumberOfPlayers = 0;
	public int mControlledAvatars = 2;
	
	private int mTeam1BlueScore = 0;
	private int mTeam2RedScore = 0;
	private Action UpdateScoreLabels;
	
	private int mLastTouch = 0;  // 1 = blue, 2 = red
	
	// HACK USE PUSH SYSTEM
	private TextMesh mStartTimerGUI;
	private int mTimeTillStart = 5;
	
	public int Team1BlueScore { get { return mTeam1BlueScore; } }
	public int Team2RedScore { get { return mTeam2RedScore; } }
	public int LastTouch { get { return mLastTouch; } }

	public override void OnLevelLoaded ()
	{
		if (mControlledAvatars == 1) {
			Transform players = GameObject.Find ("Players").transform;

			foreach (Transform child in players) {
				if (child.GetComponent<PlayerScript>().mPlayableID == 2) {
					child.gameObject.SetActive(false);
				}
			}
		}

		Time.timeScale = 1;
		mTimeTillStart = 3;
		mStartTimerGUI = GameObject.Find ("StartTimer").transform.GetComponent<TextMesh> ();
		mStartTimerGUI.text = "3";
		SingletonObject.Get.getSoundManager().play("count");
		
		SingletonObject.Get.getTimer ().Add ("GameModeStart", GuiStartCounter, 1.0f, false, mTimeTillStart, StartGameModeLoadProcess);
	}

	void GuiStartCounter() {
		mTimeTillStart --;
		mStartTimerGUI.text = mTimeTillStart.ToString ();
		if (mTimeTillStart > 0) SingletonObject.Get.getSoundManager().play("count");
	}

	public override void ResetLevelDNU ()
	{
		mTeam1BlueScore = 0;
		mTeam2RedScore = 0;
		mLastTouch = 0;
	}

	public override void LoadGameMode()
	{
		mStartTimerGUI.transform.gameObject.SetActive (false);
		
		// Enable the ball
		GameObject ball = GameObject.Find ("Ball");
		ball.rigidbody.isKinematic = false;
		ball.rigidbody.AddForce (Vector3.down);
		
		SingletonObject.Get.getSoundManager().play("go");
	}

	public override void PreGameStartEntry ()
	{

	}

	public override void PostGameStartEntry ()
	{

	}


	public void AddScore(int team) {
		if (team == 1) {
			mTeam1BlueScore ++;
		} else if (team == 2) {
			mTeam2RedScore ++;
		}
		
		// Update the score boards
		if (UpdateScoreLabels != null) {
			UpdateScoreLabels ();
		}
		
		RoundOver (team);
	}

	public void RoundOver(int teamWhoWon) {
		Time.timeScale = 0.3f;
		mStartTimerGUI.characterSize = 10;
		if (teamWhoWon == 1) {
			// Blue won.
			if (mLastTouch == 1) {
				mStartTimerGUI.text = "BLUE SCORED!";
				SingletonObject.Get.getSoundManager().play("score");
			}
			else {
				mStartTimerGUI.text = "RED OWN GOAL!";
				SingletonObject.Get.getSoundManager().play("boo");
			}
		} else if (teamWhoWon == 2) {
			// Red Won
			if (mLastTouch == 2) {
				mStartTimerGUI.text = "RED SCORED!";
				SingletonObject.Get.getSoundManager().play("score");
			}
			else {
				mStartTimerGUI.text = "BLUE OWN GOAL!";
				SingletonObject.Get.getSoundManager().play("boo");
			}
			
		}
		mStartTimerGUI.transform.gameObject.SetActive (true);

		// Can put endless mode here:
		if (mTeam2RedScore >= MAX_GOALS || mTeam1BlueScore >= MAX_GOALS) {
				SingletonObject.Get.getTimer ().Add ("acknowledgeWinner", AcknowledgeWinner, 0.7f, false);
			return;
		}

		SingletonObject.Get.getTimer ().Add ("newRound", ResetLevelWithoutResetScore, 1.0f, false);
	}

	void AcknowledgeWinner() {
		if (mTeam2RedScore > mTeam1BlueScore) {
			// Red wins
			mStartTimerGUI.text = "RED WINS!!!!!";
		} else {
			// Blue wins
			mStartTimerGUI.text = "BLUE WINS!!!!!";
		}
		SingletonObject.Get.getTimer ().Add ("newRound", ReturnToMainMenu, 0.7f, false);
		
	}

	public override void ReturnToMainMenu() {
		SingletonObject.Get.getInputManager ().ClearAllActions ();
		SingletonObject.Get.getGameState ().JumpToState (GameState.GAMESTATE.MAINMENU);
	}

	// Message sender to reset the game without resetting the score.
	public void ResetLevelWithoutResetScore() {
		mStartTimerGUI.transform.gameObject.SetActive(false);
		SingletonObject.Get.getGameState ().ResetLevelWithoutResettingScore ();
	}
	
	public void UpdateLastTouch(int team) {
		mLastTouch = team;
	}
	
	public void RegisterScoreLabel(Action a) {
		UpdateScoreLabels += a;
	}
	public void DeRegisterScoreLabel(Action a) {
		UpdateScoreLabels -= a;
	}





}
