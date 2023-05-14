using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    //Audio object
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Get player's script
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);
                //Play pickup sound
                controller.PlaySound(collectedClip);
            }
        }
    }
}
