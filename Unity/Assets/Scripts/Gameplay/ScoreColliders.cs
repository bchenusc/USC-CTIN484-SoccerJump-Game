using UnityEngine;
using System.Collections;

public class ScoreColliders : MonoBehaviour {
	
	// Score sound
	public AudioClip clip;
	
	int myTeam = 0;

	void Start() {
		if (transform.name.Contains("Blue")) {
			myTeam = 1;
		}
		else if (transform.name.Contains ("Red")) {
			myTeam = 2;
		}
	}


	void OnTriggerEnter(Collider c) {
		if (c.transform.CompareTag("Ball")) {
			SingletonObject.Get.getSoundManager().play(clip);
			if (myTeam == 1) {
				// Add a score to the red team if you score against the blue team.
				SingletonObject.Get.getGameState().AddScore(2);
			} else {
				SingletonObject.Get.getGameState().AddScore(1);
			}
		}
	}
}
