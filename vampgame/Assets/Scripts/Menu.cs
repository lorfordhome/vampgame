using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /*	START OF MAIN MENU BUTTONS SCRIPT	*/




    /*	FUNCTIONS	*/


    //using scene index instead of name, can be changed if using numbers becomes confusing

    public GameObject controlsScreen;
	public GameObject creditsScreen;
    public void StartButton()
	{
		SceneManager.LoadScene(1); 
		Debug.Log ("Start Game");
	}

	public void OptionsButton()
	{
		//SceneManager.LoadScene("Options_Menu");	DEFUNCT AS NO LONGER USING A SEPARATE SCENE FOR OPTIONS
		//Debug.Log("Options");
	}

	public void CreditsButton()
	{
		if (creditsScreen.activeInHierarchy == true)	//if creditsScreen is visible...
		{
			creditsScreen.SetActive(false);			//...make it not visible
		}
		else
		{
			creditsScreen.SetActive(true);			//...or if it isnt visible, make it visible
		}
	}

	public void HomeButton()
	{
		SceneManager.LoadScene("Main_Menu");
		Debug.Log("Home");
	}

	public void ControlsButton()
	{
		if (controlsScreen.activeInHierarchy == true)	//if controlsScreen is visible...
		{
			controlsScreen.SetActive(false);			//...make it not visible
		}
		else
		{
			controlsScreen.SetActive(true);				//...or if it isnt visible, make it visible
		}
    }

	public void QuitButton()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

}
