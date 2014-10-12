using UnityEngine;
using System.Collections;

public class PlayerConfig {
	private int mPlayerNumber;
	private KeyCode mJump;
	private KeyCode mLeft;
	private KeyCode mRight;

	public int PlayerNumber { get { return mPlayerNumber; } set { mPlayerNumber = value; } }
	public KeyCode Jump { get { return mJump; } set { mJump = value; }}
	public KeyCode Left { get { return mLeft; } set { mLeft = value; }}
	public KeyCode Right { get { return mRight; } set { mRight = value; }}

	public PlayerConfig (int playerNum, KeyCode left, KeyCode jump, KeyCode right) {
		mPlayerNumber = playerNum;
		Jump = jump;
		Left = left;
		Right = right;
	}

	public string PrintKey(int key) {
		// 0 - left
		// 1 - jump
		// 2 - right
		switch (key) {
		case 0: return KeyUtils.ConvertToReadable(Left);
		case 1: return KeyUtils.ConvertToReadable(Jump);
		case 2: return KeyUtils.ConvertToReadable(Right);
		default: return "";
		}
	}



	public string ToString() {
		return "Player " + mPlayerNumber + " Controls: [" + mLeft.ToString () + "] ["  + mJump.ToString () + "] [" + mRight.ToString () + "]";
	}
}
