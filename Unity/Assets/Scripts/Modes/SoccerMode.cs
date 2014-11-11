using UnityEngine;
using System.Collections;

public class SoccerMode : GameMode {
	private const int MAX_GOALS = 5;

	public int mNumberOfPlayers = 0;
	public int mObjectSpawnerActive = 0; // 0 Active, 1 No Active
	
	private int mTeam1BlueScore = 0;
	private int mTeam2RedScore = 0;
	private Action UpdateScoreLabels;

	Transform[] redCameras = new Transform[3];
	Transform[] blueCameras = new Transform[3];
	Camera mainCamera;
	int cameraIndex = 0;
	bool redScored = false;
	
	private int mLastTouch = 0;  // 1 = blue, 2 = red
	
	// HACK USE PUSH SYSTEM
	private TextMesh mStartTimerGUI;
	private int mTimeTillStart = 5;
	
	public int Team1BlueScore { get { return mTeam1BlueScore; } }
	public int Team2RedScore { get { return mTeam2RedScore; } }
	public int LastTouch { get { return mLastTouch; } }

	public override void OnLevelLoaded ()
	{
		if (GameObject.Find ("StartTimer") == null)
			return;
		if (SingletonObject.Get.getGameState ().mGameState != GameState.GAMESTATE.SOCCER_GAME)
			return;


		Time.timeScale = 1;
		mTimeTillStart = 3;

		if (mObjectSpawnerActive == 1) {
			GameObject.Find ("ObjectSpawner").SetActive(false);
		} else {
			GameObject.Find ("ObjectSpawner").SetActive(true);
		}
		mStartTimerGUI = GameObject.Find ("StartTimer").transform.GetComponent<TextMesh> ();
		if (mStartTimerGUI == null) return;
		mStartTimerGUI.text = "3";
		SingletonObject.Get.getSoundManager().play("Audio/count");
		
		SingletonObject.Get.getTimer ().Add ("GameModeStart", GuiStartCounter, 1.0f, false, mTimeTillStart, StartGameModeLoadProcess);

		Transform temp = GameObject.Find ("RedCamera").transform;
		// Find all the switchable cameras.
		for (int i=0; i<3;i++)
		{
			redCameras[i] = temp.GetChild(i);
		}
		temp = GameObject.Find ("BlueCamera").transform;
		// Find all the switchable cameras.
		for (int i=0; i<3;i++)
		{
			blueCameras[i] = temp.GetChild(i);
		}

	}

	void GuiStartCounter() {
		mTimeTillStart --;
		mStartTimerGUI.text = mTimeTillStart.ToString ();
		if (mTimeTillStart > 0) SingletonObject.Get.getSoundManager().play("Audio/count");
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

		// Enable all the players
		Transform players = GameObject.Find ("Players").transform;
		foreach (Transform child in players)
		{
			if (child.rigidbody)
			child.rigidbody.isKinematic = false;
		}
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").camera;
		SingletonObject.Get.getSoundManager().play("Audio/go");
	}

	public override void PreGameStartEntry ()
	{

	}

	public override void PostGameStartEntry ()
	{

	}

	void PlayerCameraSnap()
	{
		SingletonObject.Get.getSoundManager ().play ("Audio/menu_beep_1");
	}

	public void CameraSwitcher(){
		mainCamera.enabled = false;
		PlayerCameraSnap ();
		if (!redScored)
		{
			redCameras[cameraIndex].gameObject.SetActive(true);
			SingletonObject.Get.getTimer().Add("camera_changer", SwitchCamera, 0.3f, false, 3, SwitchToMainCamera); 
		} else {
			blueCameras[cameraIndex].gameObject.SetActive(true);
			SingletonObject.Get.getTimer().Add("camera_changer", SwitchCamera, 0.3f, false, 3, SwitchToMainCamera); 
		}
	}

	void SwitchCamera()
	{
		PlayerCameraSnap ();
		if (!redScored)
		{
			if (cameraIndex + 1 < 3)
			redCameras[cameraIndex + 1].gameObject.SetActive(true);
			redCameras[cameraIndex].gameObject.SetActive(false);
		} else 
		{
			if (cameraIndex + 1 < 3)
			blueCameras[cameraIndex + 1].gameObject.SetActive(true);
			blueCameras[cameraIndex].gameObject.SetActive(false);
		}

		++cameraIndex;
		if (cameraIndex >= 3)
						cameraIndex = 2;
	}

	void SwitchToMainCamera()
	{
		mainCamera.enabled = true;
		if (redScored)
		{
			redCameras[cameraIndex].gameObject.SetActive(false);
			cameraIndex = 0;
		} else 
		{
			blueCameras[cameraIndex].gameObject.SetActive(false);
			cameraIndex = 0;
		}
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

	public void PlayCheer()
	{
		SingletonObject.Get.getSoundManager().play("Audio/cheer", false, 0.3f);
	}

	public void PlayBoo()
	{
		SingletonObject.Get.getSoundManager().play("Audio/boo", false, 0.3f);
	}

	public void RoundOver(int teamWhoWon) {
		Time.timeScale = 0.25f;
		mStartTimerGUI.characterSize = 10;
		if (teamWhoWon == 1) {
			// Blue won.
			if (mLastTouch == 1) {
				mStartTimerGUI.text = "BLUE SCORED!";
				redScored = false;
				PlayCheer ();
			}
			else {
				mStartTimerGUI.text = "RED OWN GOAL!";
				redScored = false;
				PlayBoo();
			}
		} else if (teamWhoWon == 2) {
			// Red Won
			if (mLastTouch == 2) {
				mStartTimerGUI.text = "RED SCORED!";
				redScored = true;
				PlayCheer();
			}
			else {
				mStartTimerGUI.text = "BLUE OWN GOAL!";
				redScored = true;
				PlayBoo();
			}	
		}
		AddToMetrics(teamWhoWon, mLastTouch);
		mStartTimerGUI.transform.gameObject.SetActive (true);
		SingletonObject.Get.getTimer ().Add ("switcherstarter", CameraSwitcher, 0.2f, false);

		// Can put endless mode here:
		if ((mTeam2RedScore >= MAX_GOALS && mTeam2RedScore > mTeam1BlueScore + 1) || (mTeam1BlueScore >= MAX_GOALS && mTeam1BlueScore > mTeam2RedScore + 1)) {
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
		//SingletonObject.Get.getInputManager ().ClearAllActions ();
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

	private void AddToMetrics(int teamWhoWon, int mLastTouch) {
		string scorerStr;
		if (mObjectSpawnerActive == 0) scorerStr = "random obj, ";
		else scorerStr = "no random obj, ";
		if (teamWhoWon == 1) {
			// Blue won.
			if (mLastTouch == 1) {
				scorerStr += "blue scored ";
			}
			else {
				scorerStr += "red own goal ";
			}
		} else if (teamWhoWon == 2) {
			// Red Won
			if (mLastTouch == 2) {
				scorerStr += "red scored ";
			}
			else {
				scorerStr += "blue own goal ";
			}
		}
		scorerStr += (mTeam1BlueScore + "-" + mTeam2RedScore);
		SingletonObject.Get.getMetricManager().setScorer(scorerStr);
		Debug.Log (scorerStr);
		SingletonObject.Get.getMetricManager().GenerateEntry();
	}

}
