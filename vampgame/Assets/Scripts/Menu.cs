using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	/*	START OF MAIN MENU BUTTONS SCRIPT	*/




	/*	FUNCTIONS	*/
	public void StartButton()
	{
		SceneManager.LoadScene(1); 
	}

	public void OptionsButton()
	{
		//SceneManager.LoadScene(2);
	}

	public void QuitButton()
	{
		Application.Quit();
	}

}
