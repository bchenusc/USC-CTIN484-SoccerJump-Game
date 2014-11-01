using UnityEngine;
using System.Collections;

public class PlayerGameState : MonoBehaviour {

	public Vector3 mMenuPosition;
	public Vector3 mSoccerGamePosition;

	void Start()
	{
		mMenuPosition = transform.position;
	}
}
