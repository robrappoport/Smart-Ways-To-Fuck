using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalController : MonoBehaviour {

	private static GlobalController thisInstance = null;
	public int playerScore;
	public int playerLosses;
	public int addedScore;
	public int addedLoss;
	public int totalLives = 3;
	public float totalTime = 5f;
	public float currentTime;
	public float rateOfTime = 1f;
	public bool gameIsOver;
	private bool SceneIsLoaded;
	private bool SceneIsOver;
	string [] scenes = new string[5] {"SceneOne", "SceneTwo", "SceneThree", "SceneFour", "SceneFive"};
	// Use this for initialization

	public static GlobalController Instance {
		get {

			if (thisInstance == null) {
				thisInstance = FindObjectOfType (typeof(GlobalController)) as GlobalController;
		} 
			if (thisInstance == null) {
				GameObject obj = new GameObject ("GameManager");
				thisInstance = obj.AddComponent (typeof(GlobalController)) as GlobalController;
			}

			return thisInstance;
	}
	}

	void OnApplicationQuit ()
	{
		thisInstance = null;
	}

	void Awake ()
	{
		DontDestroyOnLoad (this);
		SceneIsLoaded = true;
		SceneIsOver = false;
		currentTime = totalTime;
	}
	// Update is called once per frame
	void Update () {

		if (SceneIsOver) {
			SceneIsLoaded = false;
			timeReset ();
			//give score and add losses
			Invoke ("SceneSwitch", 1f);
			Debug.Log (currentTime);
		}

		if (SceneIsLoaded) 
		{
			currentTime -= rateOfTime * Time.deltaTime;
			Debug.Log (currentTime);
			if (currentTime <= 0 && SceneIsOver == false) {
				SceneIsOver = true;
				timeReset ();
			}
		}
		
	}
		

	public void playerScoreUpdate ()
	{
		playerScore = playerScore + addedScore;
	}

	public void playerLossesUpdate ()
	{
		playerLosses = playerLosses + addedLoss;

		if (playerLosses >= totalLives)
		{
			gameIsOver = true;
		}
	}

	public void timeReset()
	{
		currentTime = totalTime;
	}

	public void SceneSwitch ()
	{
		if (gameIsOver) {
			SceneManager.LoadScene ("GameOverScene");
		} 
		else if (SceneIsOver == true)
		{
			SceneIsLoaded = true;
			SceneIsOver = false;
			int random = Random.Range (0, 4);
			SceneManager.LoadScene (scenes [random]);
		}

	}
}
