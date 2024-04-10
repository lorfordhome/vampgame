using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinCountdown : MonoBehaviour
{
	[SerializeField]
	private float winTime = 90f; //sets timer length in seconds (initialised but can be changed in scene manager)


	[SerializeField] 
	TextMeshProUGUI timerText;	//asset to use as timer

	[SerializeField] 
	private float remainingTime = 90f;	//sets remaining time in seconds (initialised but can be changed in scene manager)

	[SerializeField]
	private string sceneToLoad;
	

	private void Update()
	{
		winTime -= Time.deltaTime;
		remainingTime -= Time.deltaTime;	//updates elapsed time in "real" time
		int minutes = Mathf.FloorToInt(remainingTime / 60);		//coverts time into minutes
		int seconds = Mathf.FloorToInt(remainingTime % 60);		//converts time into seconds
		timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);	//sets the timer format

		if (winTime <= 0.1f)	//set 0.1 as if set to 0, timer goes into negatives
		{
			SceneManager.LoadScene(sceneToLoad);	//sets scene to load, sets in scene manager
		}
	}

}
