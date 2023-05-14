using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	// Play button
	public void PlayGame()
	{
		SceneManager.LoadScene("Tutorial");
	}
	// Quit button
	public void QuitGame()
    {
		Application.Quit();
    }
}
