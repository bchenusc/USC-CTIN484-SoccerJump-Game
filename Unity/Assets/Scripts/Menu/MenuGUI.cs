using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	bool toggleBool;

	private int selectionGridInt = 1;
	private string[] selectionStrings = {"1 Avatar", "2 Avatars"};

	public Transform P1Extra;
	public Transform P2Extra;

	
	void OnGUI () {

		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2 - 15, 100, 30), "PLAY")) {
			// This code is executed when Play is clicked
			SingletonObject.Get.getGameState().JumpToStateWPlayers(GameState.GAMESTATE.SOCCER_GAME, selectionGridInt+1);
		}

		selectionGridInt = GUI.SelectionGrid (new Rect (Screen.width/2-50, Screen.height/2 + 100, 100, 60), selectionGridInt, selectionStrings, 1);

		if (selectionGridInt == 0) {
			P1Extra.gameObject.SetActive(false);
			P2Extra.gameObject.SetActive(false);
		} else {
			P1Extra.gameObject.SetActive(true);
			P2Extra.gameObject.SetActive(true);
		}
		
	}
	
}
