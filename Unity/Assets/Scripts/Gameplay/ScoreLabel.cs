using UnityEngine;
using System.Collections;

public class ScoreLabel : GameplayObject {

	int myTeam = -1;

	// Use this for initialization
	void Start () {
		if (this == null) return;
		
		SingletonObject.Get.getGameState ().RegisterScriptAsGameplayObject (this);
		if (transform.name.Contains("Blue")) {
			myTeam = 1;
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.Team1BlueScore.ToString();
			
		}
		else if (transform.name.Contains ("Red")) {
			myTeam = 2;
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.Team2RedScore.ToString();
			
		}
	}

	public override void GameStart() {
		if (this == null) return;
		
		RegisterUpdateText ();
	}

	void OnDestroy() {
		DeRegisterUpdateText ();
	}

	void RegisterUpdateText() {
		if (this == null) return;
		
		SingletonObject.Get.getGameState ().GET_MODE_AS_SOCCER.RegisterScoreLabel (UpdateText);
	}

	void DeRegisterUpdateText() {
		if (this == null) return;

		SingletonObject s = SingletonObject.Get;
		if (s != null) {
			SoccerMode mode = s.getGameState().GET_MODE_AS_SOCCER;
			if (mode != null) {
				mode.DeRegisterScoreLabel (UpdateText);
			}
		}
	}
	
	void UpdateText() {
		if (this == null) return;
		if (myTeam == 1){
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.Team1BlueScore.ToString();
		} else if (myTeam == 2) {
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().GET_MODE_AS_SOCCER.Team2RedScore.ToString();
		}

	}
}
