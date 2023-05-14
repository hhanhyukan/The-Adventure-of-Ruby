using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thanks : MonoBehaviour
{
    // When player click the "End" button in Thanks for playing scene
    public void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
