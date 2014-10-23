using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {
	
	bool toggleBool;
	
	private int selectionGridInt = 0;
	private string[] selectionStrings = {"Random Objects ON", "Random Objects OFF"};

	
	void OnGUI () {
		
		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2 + 30, 100, 30), "PLAY")) {
			// This code is executed when Play is clicked
			SingletonObject.Get.getGameState().JumpToStateWPlayers(GameState.GAMESTATE.SOCCER_GAME, selectionGridInt);
		}
		
		selectionGridInt = GUI.SelectionGrid (new Rect (Screen.width/2-75, Screen.height/2 + 100, 150, 60), selectionGridInt, selectionStrings, 1);
		
	}
	
}
