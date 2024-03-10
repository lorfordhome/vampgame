using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	/*	START OF MAIN MENU BUTTONS SCRIPT	*/




	/*	FUNCTIONS	*/


	//using scene index instead of name, can be changed if using numbers becomes confusing
	public void StartButton()
	{
		SceneManager.LoadScene(1); 
		Debug.Log ("Start Game");
	}

	public void OptionsButton()
	{
		SceneManager.LoadScene(2);
		Debug.Log("Options");
	}

	public void HomeButton()
	{
		SceneManager.LoadScene(0);
		Debug.Log("Home");
	}
	public void QuitButton()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

}
