﻿using UnityEngine;
using System.Collections.Generic;

/*
	 * How to use:
	 * 
	 * 1. Input Manager must be attached to the SingletonObject
	 * 2. Each key that you want to use must be called in RegisterKeyCode;
	 * 3. Each script that wants a callback from the Input Manager must call RegisterOnKey"Up,Down,Held"();
	 * 
	 * @Brian Chen
	 */


public class InputManager : Singleton{

	#region Events
	// Remove Unecessary
	private event ActionInt OnMouseClick;
	private event ActionInt OnMouseUp;
	private event ActionInt OnMouseHeld;
	private event ActionKey OnKeyDown;
	private event ActionKey OnKeyUp;
	private event ActionKey OnKeyHeld;
	#endregion

	#region Game Controls
	private short mCanLeftClickHeld = 0;
	private short mCanLeftClick = 0;
	private short mCanRightClick = 0;
	private short mCanMiddleClick = 0;
	private short mCanLeftClickUp = 0;
	private short mCanRightClickUp = 0;
	private short mCanMiddleClickUp = 0;
	private HashSet<KeyManager> mEnabledKeys = new HashSet<KeyManager>();
	#endregion

	#region SoccerJump Variables
	private float mMinJumpPower = 600;
	private float mExtraLiftPower = 0;
	private int mExtraLiftRepetition = 0;
	public float MinJumpPower { get { return mMinJumpPower; } set { mMinJumpPower = value;}}
	public float ExtraLiftPower { get { return mExtraLiftPower; } set { mExtraLiftPower = value;}}
	public int ExtraLiftRepetition { get { return mExtraLiftRepetition; } set { mExtraLiftRepetition = value;}}
	#endregion

	#region PlayerContorls
	public KeyCode key_p1_jump = KeyCode.G;
	public KeyCode[] key_p1_tilt;
	public KeyCode key_p2_jump = KeyCode.Period;
	public KeyCode[] key_p2_tilt;

	#endregion

	void Start() {
		key_p1_tilt = new KeyCode[4] {KeyCode.W,
			KeyCode.S, 
			KeyCode.A, 
			KeyCode.D};
		key_p2_tilt = new KeyCode[4] {KeyCode.UpArrow,
			KeyCode.DownArrow, 
			KeyCode.LeftArrow, 
			KeyCode.RightArrow};
		RegisterKeyCode (KeyCode.W, true, false, false);
		RegisterKeyCode (KeyCode.S, true, false, false);
		RegisterKeyCode (KeyCode.A, true, false, false);
		RegisterKeyCode (KeyCode.D, true, false, false);
		RegisterKeyCode (KeyCode.UpArrow, true, false, false);
		RegisterKeyCode (KeyCode.DownArrow, true, false, false);
		RegisterKeyCode (KeyCode.LeftArrow, true, false, false);
		RegisterKeyCode (KeyCode.RightArrow, true, false, false);
		RegisterKeyCode (KeyCode.G, true, false, false);
		RegisterKeyCode (KeyCode.Period, true, false, false);
	}

	// Remove Functions if not needed.
	void Update () {
		//HACK - REMOVE
		if (Input.GetKeyDown(KeyCode.R)) {
			SingletonObject.Get.getGameState().LoadLevel(0);
			return;
		}
		if (OnMouseClick != null) {
			if (mCanLeftClick > 0 && Input.GetMouseButtonDown(0)) {OnMouseClick(0);}
			if (mCanRightClick > 0 && Input.GetMouseButtonDown(1)) {OnMouseClick(1);}
			if (mCanMiddleClick > 0 && Input.GetMouseButton(2)) {OnMouseClick(2);}
		}

		if (OnMouseUp != null) {
			if (mCanLeftClick > 0 && Input.GetMouseButtonUp(0)) { OnMouseUp(0); }
			if (mCanRightClick > 0 && Input.GetMouseButtonUp(1)) { OnMouseUp(1); }
		}

		if (OnMouseHeld != null) {
			if (mCanLeftClickHeld > 0 && Input.GetMouseButtonUp(0)) { OnMouseUp(0); }
		}

		if (OnKeyDown != null) {
			foreach (KeyManager key in mEnabledKeys) {
				if (key.down && Input.GetKeyDown(key.key)) {
					OnKeyDown(key.key);
				}
			}
		}

		if (OnKeyHeld != null) {
			foreach (KeyManager key in mEnabledKeys) {
				if (key.held && Input.GetKey(key.key)) {
					OnKeyDown(key.key);
				}
			}
		}

		if (OnKeyUp != null) {
			foreach (KeyManager key in mEnabledKeys) {
				if (key.up && Input.GetKeyUp(key.key)) {
					OnKeyUp(key.key);
				}
			}
		}
	}

	#region Register Functions
	public void RegisterKeyCode(KeyCode key, bool onDown, bool onUp, bool onHeld) {
		mEnabledKeys.Add(new KeyManager(key, onDown, onHeld, onUp));
	}
	public void RegisterOnKeyDown(ActionKey a) {
		OnKeyDown += a;
	}
	public void DeregisterOnKeydown(ActionKey a) {
		OnKeyDown -= a;
	}
	public void RegisterOnKeyUp(ActionKey a) {
		OnKeyUp += a;
	}
	public void DeregisterOnKeyUp(ActionKey a) {
		OnKeyUp += a;
	}
	public void RegisterOnKeyHeld(ActionKey a) {
		OnKeyHeld += a;
	}
	public void DeregisterOnKeyHeld(ActionKey a) {
		OnKeyHeld -= a;
	}
	// -------
	public void RegisterLeftClickDown(ActionInt a) {
		mCanLeftClick ++;
		OnMouseClick += a;
	}
	public void DeregisterLeftClickDown(ActionInt a) {
		mCanLeftClick --;
		OnMouseClick -= a;
	}
	public void RegisterLeftClickHeld(ActionInt a) {
		mCanLeftClickHeld ++;
		OnMouseHeld += a;
	}
	public void DeregisterLeftClickHeld(ActionInt a) {
		mCanLeftClick --;
		OnMouseHeld -= a;
	}
	public void RegisterLeftClickUp(ActionInt a) {
		mCanLeftClickUp ++;
		OnMouseUp += a;
	}
	public void DeregisterLeftClickUp(ActionInt a) {
		mCanLeftClickUp --;
		OnMouseUp -= a;
	}
	// -------- 
	public void RegisterRightClickDown(ActionInt a) {
		mCanRightClick ++;
		OnMouseClick += a;
	}
	public void DeregisterRightClickDown(ActionInt a) {
		mCanRightClick --;
		OnMouseClick -= a;
	}
	public void RegisterRightClickUp(ActionInt a) {
		mCanRightClickUp ++;
		OnMouseClick += a;
	}
	public void DeregisterRightClickUp(ActionInt a) {
		mCanRightClickUp --;
		OnMouseClick -= a;
	}
	public void RegisterMiddleClick(ActionInt a) {
		mCanMiddleClick ++;
		OnMouseClick += a;
	}
	public void DeregisterMiddleClick(ActionInt a) {
		mCanMiddleClick --;
		OnMouseClick -= a;
	}
	public void RegisterMiddleClickUp(ActionInt a) {
		mCanMiddleClickUp ++;
		OnMouseClick += a;
	}
	public void DeregisterMiddleClickUp(ActionInt a) {
		mCanMiddleClickUp --;
		OnMouseClick -= a;
	}
	#endregion

	#region Inherited functions
	protected override void DestroyIfMoreThanOneOnObject(){
		if (transform.GetComponents<InputManager>().Length > 1){
			Debug.Log ("Destroying Extra " + this.GetType() + " Attachment");
			DestroyImmediate(this);
		}
	}
	#endregion

	public struct KeyManager{
		public KeyCode key;
		public bool down;
		public bool up;
		public bool held;
		
		public KeyManager(KeyCode k, bool buttonDown, bool buttonHeld, bool buttonUp) {
			key = k;
			down = buttonDown;
			held = buttonHeld;
			up = buttonUp;
		}

		public override int GetHashCode() {
			return key.GetHashCode();
		}
		
		public override bool Equals(object obj) {
			if (!(obj is KeyManager))
				return false;
			KeyManager other = (KeyManager)obj;
			return key == other.key;
		}
	}
}







