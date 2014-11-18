using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	bool gameStart = false;

	void Start()
	{
		gameStart = false;
	}

	void OnMouseUp () {
		StartGame ();
	}

	void StartGame()
	{
		if (!gameStart)
		{
			gameStart = true;
			// This code is executed when Play is clicked
			SingletonObject.Get.getSoundManager().play("Audio/menu_beep_1");
			AddToMetrics();
			SingletonObject.Get.getGameState().JumpToStateWPlayers(GameState.GAMESTATE.SOCCER_GAME, 1);
		}
	}
	
	void OnCollisionEnter(Collision c)
	{
		StartGame ();
	}
	
	void AddToMetrics()
	{
		SingletonObject.Get.getMetricManager().setScorer("menu screen");
		SingletonObject.Get.getMetricManager().GenerateEntry();
	}
}
