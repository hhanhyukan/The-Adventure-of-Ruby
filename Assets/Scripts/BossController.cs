using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public int BossmaxHealth = 20;
    public int health { get { return currentHealth; } }
    public ParticleSystem hitEffect;
    public AudioSource bosstheme, peacetheme;
    public AudioClip hit;

    public Canvas canvas;
    Rigidbody2D rigidbody2D;
    AudioSource audio;
    bool alive = true;
    int currentHealth;
    Animator bossAnimation;

    void Start()
    {
        // Get Unity Animation
        bossAnimation = gameObject.GetComponent<Animator>();
        currentHealth = BossmaxHealth;
        // Get rigid body
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        // Get audio source
        audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // If the boss still alive
        if (!alive)
        {
            bossAnimation.enabled = false;
            isKilled();
        }
        // Stop sound footstep when the game is paused
        if (PauseMenu.GameisPaused)
        {
            audio.pitch *= .5f;
        }
        // Continue footstep when the game is resumed
        if (!PauseMenu.GameisPaused)
        {
            audio.pitch = 1f;
        }
    }

    public void LookAtPLayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        // Rotate to look at player
        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            canvas.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            canvas.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    // Contact with player
    void OnTriggerStay2D(Collider2D other)
    {
        // Get player reference
        RubyController player = other.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
    // Being hited by the bullet
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile bullet = other.GetComponent<Projectile>();
        // If the Boss is hit by the player's projectile, then deduce it HP by 1
        if (bullet != null)
        {
            ChangeBossHealth(-1);
            hitEffect.Play();
            audio.PlayOneShot(hit);
            Debug.Log("Boss health: " + currentHealth);
            UIBossHealthBar.instance.SetValue(currentHealth / (float)BossmaxHealth);
        }
    }

    // When the Boss HP is zero, it stop moving
    public void Die()
    {
        rigidbody2D.simulated = false;
        alive = false;
        audio.Stop();
        bosstheme.Stop();
        peacetheme.Play();
    }
    // Change boss health
    public void ChangeBossHealth(int amount)
    {
        // Change health value
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, BossmaxHealth);
        KillCheck();
    }
    // Check if boss is killed
    public void KillCheck()
    {
        if(currentHealth == 0)
        {
            Die();
        }
    }
    // Return boolean value
    public bool isKilled()
    {
        if (alive)
        {
            return false;
        }
        return true;
    }
}
