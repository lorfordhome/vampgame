using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    /*	START OF MAIN MENU BUTTONS SCRIPT	*/




    /*	FUNCTIONS	*/


    //using scene index instead of name, can be changed if using numbers becomes confusing

    public GameObject controlsScreen;
    public void StartButton()
	{
		SceneManager.LoadScene(1); 
		Debug.Log ("Start Game");
	}

	public void OptionsButton()
	{
		SceneManager.LoadScene("Options_Menu");
		Debug.Log("Options");
	}

	public void HomeButton()
	{
		SceneManager.LoadScene("Main_Menu");
		Debug.Log("Home");
	}

	public void ControlsButton()
	{
		if (controlsScreen.active == true)
		{
			controlsScreen.SetActive(false);
		}
		else
		{
			controlsScreen.SetActive(true);
		}
    }

	public void QuitButton()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

}
