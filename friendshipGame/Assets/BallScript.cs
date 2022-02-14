using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	private Vector2 ballPosition;
	private Vector2 releasePosition;
	private bool didMouseDown;
	private bool didMouseUp;
	public float forceMulitplier;
	public float totalDistance;
	public int strokes;
	private float ballRadius;


	void ResetMouse ()
	{
		didMouseUp = false;
		didMouseDown = false;
	}

	void Start ()
	{
		ResetMouse();
		//figure out how to reference the game object's circle collider
		ballRadius = 0.5f;
		ballPosition = gameObject.transform.position;
		totalDistance = 0f;
		strokes = 0;
	}

	void OnMouseDown()
	{
		ballPosition = gameObject.transform.position;
		didMouseDown = true;
	}

	void OnMouseUp()
	{
		releasePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		didMouseUp = true;		
	}

	void Update ()
	{

		//IF the mouse button was clicked inside and then outside of the ball's raius then add force
		if ((didMouseUp && didMouseDown) && (Vector2.Distance(ballPosition, releasePosition) > ballRadius))
		{
			GetComponent<Rigidbody2D>().AddForce ((ballPosition - releasePosition) * forceMulitplier);
			Debug.Log ("total force before multiplier: " + (ballPosition - releasePosition)* forceMulitplier);
			// GetComponent<AudioSource>().Play ();
			ResetMouse();
			GameControl.control.strokes++;
			var gameInst = GameObject.Find("Instructions");
			Destroy(gameInst);
		}

		if (didMouseDown) 
		{
		
		}



		Vector2 currentPosition = gameObject.transform.position;

		if (ballPosition != currentPosition) 
		{
			GameControl.control.totalDistance += Vector2.Distance(currentPosition,ballPosition);
			ballPosition = currentPosition;
		}

	}

}
