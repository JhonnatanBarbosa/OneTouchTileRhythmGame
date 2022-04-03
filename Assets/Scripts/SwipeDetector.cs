using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
	private Vector2 fingerDownPos;
	private Vector2 fingerUpPos;

	public bool detectSwipeAfterRelease = false;

    public GameObject Player;
	public GameObject EmptyStart;
	public GameObject EmptyEnd;
	private float desiredDuration = 0.2f;
	private float elapsedTime;

	public float SWIPE_THRESHOLD = 20f;

	void Start() {
		

	}

	// Update is called once per frame
	void Update ()
	{
		elapsedTime += Time.deltaTime;
		float percentageComplete = elapsedTime / desiredDuration;

		Player.transform.position = Vector3.Lerp(EmptyStart.transform.position, EmptyEnd.transform.position, percentageComplete);

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				fingerUpPos = touch.position;
				fingerDownPos = touch.position;
			}

			//Detects Swipe while finger is still moving on screen
			if (touch.phase == TouchPhase.Moved) {
				if (!detectSwipeAfterRelease) {
					fingerDownPos = touch.position;
					DetectSwipe ();
				}
			}

			//Detects swipe after finger is released from screen
			if (touch.phase == TouchPhase.Ended) {
				fingerDownPos = touch.position;
				DetectSwipe ();
			}
		}
	}

	void DetectSwipe ()
	{
		
		if (VerticalMoveValue () > SWIPE_THRESHOLD && VerticalMoveValue () > HorizontalMoveValue ()) {
			Debug.Log ("Vertical Swipe Detected!");
			if (fingerDownPos.y - fingerUpPos.y > 0) {
				OnSwipeUp ();
			} else if (fingerDownPos.y - fingerUpPos.y < 0) {
				OnSwipeDown ();
			}
			fingerUpPos = fingerDownPos;

		} else if (HorizontalMoveValue () > SWIPE_THRESHOLD && HorizontalMoveValue () > VerticalMoveValue ()) {
			Debug.Log ("Horizontal Swipe Detected!");
			if (fingerDownPos.x - fingerUpPos.x > 0) {
				OnSwipeRight ();
			} else if (fingerDownPos.x - fingerUpPos.x < 0) {
				OnSwipeLeft ();
			}
			fingerUpPos = fingerDownPos;

		} else {
			Debug.Log ("No Swipe Detected!");
		}
	}

	float VerticalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.y - fingerUpPos.y);
	}

	float HorizontalMoveValue ()
	{
		return Mathf.Abs (fingerDownPos.x - fingerUpPos.x);
	}

	void OnSwipeUp ()
	{	
		//Do something when swiped up

		elapsedTime = 0;

		EmptyStart.transform.position = Player.transform.position;
		EmptyEnd.transform.position = Player.transform.position + new Vector3(0,0,10f);

	}

	void OnSwipeDown ()
	{
		//Do something when swiped down

        elapsedTime = 0;

		EmptyStart.transform.position = Player.transform.position;
		EmptyEnd.transform.position = Player.transform.position + new Vector3(0,0,-10f);
	}

	void OnSwipeLeft ()
	{
		//Do something when swiped left

        elapsedTime = 0;

		EmptyStart.transform.position = Player.transform.position;
		EmptyEnd.transform.position = Player.transform.position + new Vector3(-10f,0,0);
	}

	void OnSwipeRight ()
	{
		//Do something when swiped right

        elapsedTime = 0;

		EmptyStart.transform.position = Player.transform.position;
		EmptyEnd.transform.position = Player.transform.position + new Vector3(10f,0,0);
	}
}