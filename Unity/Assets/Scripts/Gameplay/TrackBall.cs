using UnityEngine;
using System.Collections;

public class TrackBall : MonoBehaviour {

	public Transform ballFollow;
	Vector3 tempPosition;
	MeshRenderer myRender;

	void Start()
	{
		myRender = transform.GetComponent<MeshRenderer> ();
	}

	void Update()
	{
		tempPosition = transform.position;
		 tempPosition.Set (ballFollow.position.x, transform.position.y, transform.position.z);
		transform.position = tempPosition;

		if (ballFollow.transform.position.y > transform.position.y)
		{
			myRender.enabled = true;
		} else 
		{
			myRender.enabled = false;
		}
	}
}
