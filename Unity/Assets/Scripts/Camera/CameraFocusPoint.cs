using UnityEngine;
using System.Collections.Generic;

public class CameraFocusPoint : MonoBehaviour {

	// Place this on the focal point object which will be childed under Players
	LinkedList<Transform> players = new LinkedList<Transform>();
	Vector3 initialPosition;
	Vector3 GotoLocation;

	void Start()
	{
		initialPosition = transform.position;
		players.Clear ();
		// Grab a list of the players.
		foreach (Transform child in transform.parent)
		{
			if (child != transform)
			{
				players.AddLast(child);
			}
		}
	}

	void Update()
	{
		// Keep the focal point between all the players.
		AverageVectorsOfPlayers ();
		transform.position = Vector3.MoveTowards(transform.position, GotoLocation, Time.deltaTime * 2);


	}

	void AverageVectorsOfPlayers()
	{
		Vector3 newLocation = Vector3.zero;
		int playersTracked = 0;

		foreach (Transform t in players)
		{
			if ((Mathf.Abs(t.position.x)) < initialPosition.x + 14  && t.position.y > -3)
			{
				//Debug.Log (t.position.x);
				newLocation += t.position;
				playersTracked ++;
			}
		}
		if (playersTracked != 0)
		newLocation /= playersTracked;
		newLocation.Set(newLocation.x, initialPosition.y, initialPosition.z);
		GotoLocation = newLocation;
	}
}









