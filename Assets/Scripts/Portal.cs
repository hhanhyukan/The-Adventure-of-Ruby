using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void Start()
    {
        // Disable portal at the beginning
        gameObject.SetActive(false);
    }

    public void enable()
    {
        // Enable portal
        gameObject.SetActive(true);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Get player reference
        RubyController controller = other.GetComponent<RubyController>();
        // If the player step into the portal
        if (controller != null)
        {
            PlayerPrefs.SetInt("health", controller.health);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
