using UnityEngine;
using System.Collections;

public class ShowControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int playerNum = 0;
		Transform parent = transform.parent;
		PlayerScript pScript;
		// Find the player's number.
		// We will never have a hiearchy that is greater than 10 parents.
		for (int i=0; i<10 ; i++ ) {
			// We are searching for the parent with the pScript.
			pScript = parent.GetComponent<PlayerScript>();
			if (pScript != null) {
				playerNum = pScript.PlayerNumber;
				break;
			} 
			else {
				parent = parent.parent;
			}
		}

		string control = transform.name;
		if (control.Equals("U")) {
			// Set jump values.
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getInputManager().mPlayers[playerNum-1].PrintKey(1);
		} else if (control.Equals("R")) {
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getInputManager().mPlayers[playerNum-1].PrintKey (2);
			
		} else if (control.Equals("L")) {
			transform.GetComponent<TextMesh>().text = SingletonObject.Get.getInputManager().mPlayers[playerNum-1].PrintKey(0);
			
		}


	}

	private void HideControls() {
		// TODO Add a fade out animation here in the future.
		if (this == null)
						return;
		//transform.parent.gameObject.SetActive (false);
	}
}
























