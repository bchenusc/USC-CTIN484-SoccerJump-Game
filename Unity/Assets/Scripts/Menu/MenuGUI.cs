using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	void OnMouseUp () {
			// This code is executed when Play is clicked
			SingletonObject.Get.getSoundManager().play("Audio/menu_beep_1");
			AddToMetrics();
			SingletonObject.Get.getGameState().JumpToStateWPlayers(GameState.GAMESTATE.SOCCER_GAME, 1);
			
	}
	
	void AddToMetrics()
	{
		SingletonObject.Get.getMetricManager().setScorer("menu screen");
		SingletonObject.Get.getMetricManager().GenerateEntry();
	}
}
