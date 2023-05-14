using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    // Go back to main menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start game
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
