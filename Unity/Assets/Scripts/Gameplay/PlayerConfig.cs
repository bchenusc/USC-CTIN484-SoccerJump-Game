using UnityEngine;
using System.Collections;

public class PlayerConfig {
	private int mPlayerNumber;
	private KeyCode mJump;
	private KeyCode mForward;
	private KeyCode mBackward;
	private KeyCode mLeft;
	private KeyCode mRight;

	public int PlayerNumber { get { return mPlayerNumber; } set { mPlayerNumber = value; } }
	public KeyCode Jump { get { return mJump; } set { mJump = value; }};
	public KeyCode Forward { get { return mForward; } set { mForward = value; }};
	public KeyCode Backward { get { return mBackward; } set { mBackward = value; }};
	public KeyCode Left { get { return mLeft; } set { mLeft = value; }};
	public KeyCode Right { get { return mRight; } set { mJump = value; }};

	public PlayerConfig (KeyCode jump, KeyCode forward, KeyCode backward, KeyCode left, KeyCode right) {
		Jump = jump;
		Forward = forward;
		Backward = backward;
		Left = left;
		Right = right;
	}
}
