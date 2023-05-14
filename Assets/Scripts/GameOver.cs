using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Try again button
    public void TryAgain()
    {
        SceneManager.LoadScene("Tutorial");
    }
    // Quit button
    public void QuitGame()
    {
        Application.Quit();
    }
}
