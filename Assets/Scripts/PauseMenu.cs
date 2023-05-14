using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingMenuUI;
    public AudioSource backgroundAudio;

    // Update is called once per frame
    void Update()
    {
        // Pause button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        // Options button
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (GameisPaused)
            {
                Resume();
            }
            else
            {
                Settings();
            }
        }
    }

    //Resume function
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        backgroundAudio.Play();
        Time.timeScale = 1f;
        GameisPaused = false;
    }

    //Pause funtion
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        backgroundAudio.Pause();
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    //Settings function
    public void Settings()
    {
        settingMenuUI.SetActive(true);
        backgroundAudio.Pause();
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    //Load main menu
    public void LoadMenu()
    {
        GameisPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
