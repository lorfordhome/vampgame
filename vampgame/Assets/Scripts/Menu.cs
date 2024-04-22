using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    /*	START OF MAIN MENU BUTTONS SCRIPT	*/




    /*	FUNCTIONS	*/


    //using scene index instead of name, can be changed if using numbers becomes confusing

    public GameObject controlsScreen;
	public GameObject creditsScreen;
	public GameObject optionsScreen;
	
	[SerializeField] Slider gameSlider;

	private void Start()
	{
		gameSlider.value = 1f;
	}
	public void MuteButton()
	{
		gameSlider.value = 0f;
		VolumeChange();

	}
	public void VolumeChange()
	{
		AudioListener.volume = gameSlider.value;
		
	}
    public void StartButton()
	{
		SceneManager.LoadScene(1); 
		Debug.Log ("Start Game");
	}

	public void OptionsButton()
	{
		if (optionsScreen.activeInHierarchy == true)    //if optionsScreen is visible...
		{
			optionsScreen.SetActive(false);         //...make it not visible
		}
		else
		{
			optionsScreen.SetActive(true);          //...or if it isnt visible, make it visible
		}
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
