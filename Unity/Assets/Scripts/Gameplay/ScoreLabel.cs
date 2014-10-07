using UnityEngine;
using System.Collections;

public class ScoreLabel : GameplayObject {

	int myTeam = -1;

	// Use this for initialization
	void Start () {
		SingletonObject.Get.getGameState ().RegisterScriptAsGameplayObject (this);
		if (transform.name.Contains("Blue")) {
			myTeam = 1;
		}
		else if (transform.name.Contains ("Red")) {
			myTeam = 2;
		}
	}

	public override void GameStart() {
		Debug.Log ("GameStart");
		RegisterUpdateText ();
	}

	void OnDestroy() {
		DeRegisterUpdateText ();
	}

	void RegisterUpdateText() {
		SingletonObject.Get.getGameState ().RegisterScoreLabel (UpdateText);
	}

	void DeRegisterUpdateText() {
		SingletonObject s = SingletonObject.Get;
		if (s != null)
			s.getGameState ().DeRegisterScoreLabel (UpdateText);
	}
	
	void UpdateText() {
		Debug.Log ("updating");
		if (myTeam == 1){
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().Team1BlueScore.ToString();
		} else if (myTeam == 2) {
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getGameState().Team2RedScore.ToString();
		}

	}
}
