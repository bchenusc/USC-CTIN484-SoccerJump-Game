using UnityEngine;
using System.Collections;

public class KeyUtils : MonoBehaviour {
	public static string ConvertToReadable(KeyCode key) {
		switch (key) {
		case KeyCode.Delete: return "DEL";
		case KeyCode.Tab: return "TAB";
		case KeyCode.Return: return "RET";
		case KeyCode.Escape: return "ESC";
		case KeyCode.Space: return "SPC";
		case KeyCode.Keypad0: return "#0";
		case KeyCode.Keypad1: return "#1";
		case KeyCode.Keypad2: return "#2";
		case KeyCode.Keypad3: return "#3";
		case KeyCode.Keypad4: return "#4";
		case KeyCode.Keypad5: return "#5";
		case KeyCode.Keypad6: return "#6";
		case KeyCode.Keypad7: return "#7";
		case KeyCode.Keypad8: return "#8";
		case KeyCode.Keypad9: return "#9";
		case KeyCode.Alpha0: return "0";
		case KeyCode.Alpha1: return "1";
		case KeyCode.Alpha2: return "2";
		case KeyCode.Alpha3: return "3";
		case KeyCode.Alpha4: return "4";
		case KeyCode.Alpha5: return "5";
		case KeyCode.Alpha6: return "6";
		case KeyCode.Alpha7: return "7";
		case KeyCode.Alpha8: return "8";
		case KeyCode.Alpha9: return "9";
		case KeyCode.Period: return ".";
		case KeyCode.Slash: return "/";
		case KeyCode.Minus: return "-";
		case KeyCode.Equals: return "=";
		case KeyCode.Backslash: return "\\";
		case KeyCode.Comma: return ",";
		case KeyCode.LeftBracket: return "[";
		case KeyCode.RightBracket: return "]";
		case KeyCode.UpArrow: return "UP";
		case KeyCode.LeftArrow: return "<-";
		case KeyCode.RightArrow: return "->";
		default: return key.ToString();
		}
	}
}
