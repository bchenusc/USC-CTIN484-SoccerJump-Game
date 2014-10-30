using UnityEngine;
using System.Collections;

public class MenuMode : GameMode {

	public override void OnLevelLoaded ()
	{
		Time.timeScale = 0.5f;

	}

	public override void ResetLevelDNU ()
	{
	}

	public override void LoadGameMode ()
	{

	}

	public override void PreGameStartEntry ()
	{
	}

	public override void PostGameStartEntry ()
	{
	}

	public override void ReturnToMainMenu() {

	}
}
