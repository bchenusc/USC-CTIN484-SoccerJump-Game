using UnityEngine;
using System.Collections.Generic;


public abstract class GameMode {

	public abstract void OnLevelLoaded();

	public abstract void ResetLevelDNU();

	protected void StartGameModeLoadProcess () {
		SingletonObject.Get.getGameState ().LoadGameMode ();
	}

	public abstract void LoadGameMode();

	// Start functions
	public abstract void PreGameStartEntry ();

	public abstract void PostGameStartEntry ();

	public abstract void ReturnToMainMenu();

}
