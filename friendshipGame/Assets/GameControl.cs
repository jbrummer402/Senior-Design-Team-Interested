using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour 
{

	public static GameControl control;

	public float totalDistance;
	public int strokes;
	public bool isHoleOver;
	public int totalNumberOfHoles = 9;

	public int[] lastStrokes;
	public int[] bestStrokes;
	public float[] lastDistance;
	public float[] highestDistance;
	public float[] lowestDistance;
	private int totals;
	private float floatTotals;


	void Awake ()
	{
		if (control == null) 
		{
			DontDestroyOnLoad (gameObject);
			control = this;
		}
		else if (control != this)
		{
			Destroy(gameObject);
		}



		//If Arrays are empty initilize them.
		if (lastStrokes.Length < 1) 
		{
			//Offset Array So array indexes match hole numbers
			totalNumberOfHoles++;
			lastStrokes = new int[totalNumberOfHoles];
			bestStrokes = new int[totalNumberOfHoles];
			lastDistance = new float[totalNumberOfHoles];
			highestDistance = new float[totalNumberOfHoles];
			lowestDistance = new float[totalNumberOfHoles];
		}


		// add up all scores and put them in array[0] - sloppy
		for (int i = 1; i < GameControl.control.totalNumberOfHoles; i++) 
		{
				totals += GameControl.control.bestStrokes[i];
		}
		GameControl.control.bestStrokes [0] = totals; 

		for (int i = 1; i < GameControl.control.totalNumberOfHoles; i++) 
		{
			floatTotals += GameControl.control.highestDistance[i];
		}
		GameControl.control.highestDistance [0] = floatTotals;

		floatTotals = 0f;

		for (int i = 1; i < GameControl.control.totalNumberOfHoles; i++) 
		{
			floatTotals += GameControl.control.lowestDistance[i];
		}
		GameControl.control.lowestDistance [0] = floatTotals;



	}


}
