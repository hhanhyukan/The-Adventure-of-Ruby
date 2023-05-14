using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AudioSource audioSource;

    public float speed = 2.0f;
    public float changeTime = 2.0f;
    public bool vertical;
    public bool isFixed;
    public ParticleSystem smokeEffect, hitEffect;
    public AudioClip hit;

    Rigidbody2D rigidbody2D;
    Animator animator;
    float timer;
    int direction = 1;
    bool broken = true;

    // The robot is firstly identified as broken. It has to be fixed in order to stop moving and became unhostile object

    // Start is called before the first frame update
    void Start()
    {      
        isFixed = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        // Get the animator controller
        animator = GetComponent<Animator>();
        //Get audio source component
        audioSource = GetComponent<AudioSource>();
    }

    // Moving back and forth
    void Update()
    {
        if (!broken)
        {
            return;
        }
        // Countdown change the moving direction
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (PauseMenu.GameisPaused)
        {
            audioSource.pitch *= .5f;
        }

        if (!PauseMenu.GameisPaused)
        {
            audioSource.pitch = 1f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the robot is alive
        if (!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;
        // Move up and down
        if (vertical)
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        // Move left and right
        else
        {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    // Damage player on interacting with the collision
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController player = other.GetComponent<RubyController>();
        Projectile bullet = other.GetComponent<Projectile>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
        // If the robot is hit by the player's projectile, then fix it imediately
        if (bullet != null)
        {
            Fix();
            hitEffect.Play();
            audioSource.PlayOneShot(hit);
            isFixed = true;
        }
    }

    // When the robot is being hit by the player's projectile then it will be fixed and stop moving
    public void Fix()
    {
        broken = false;
        // Remove rigidbody from the physic system. The robot cannot damage player anymore
        rigidbody2D.simulated = false;
        Debug.Log("Fixed");
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
        //Play audio
        audioSource.Stop();
        isKilled();
    }
    // Return boolean value
    public bool isKilled()
    {
        if (isFixed)
        {
            return true;
        }
        return false;
    }
}
