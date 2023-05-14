using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreHealth : MonoBehaviour
{
    // Store health by max health
    void Start()
    {
        PlayerPrefs.SetInt("health", 5);
    }
}
