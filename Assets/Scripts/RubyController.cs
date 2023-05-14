using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    //Audio object
    public AudioClip throwClip, hitClip;

    public int maxHealth = 5;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    public float timeCooldown = 2.0f;
    public int enemiesCount;

    private AudioSource audioSource;
    public GameObject projectilePrefab;
    public ParticleSystem shield, regen;

    int currentHealth;
    Vector2 Pos;

    public int health { get { return currentHealth; } }
    public Vector2 currentPos {get { return Pos; }}

    bool isInvincible;
    float invincibleTimer;
    bool cooldown;
    float cooldownTimer;
    bool dashCD;
    float dashCDTimer;
    float delay;
    float dash;
    float gameover;

    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 LookDirection = new Vector2(0, -1);

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = PlayerPrefs.GetInt("health");
        cooldown = false;
        dashCD = false;
        dash = 0.0f;
        delay = 0.1f;
        gameover = 0.5f;
        dashCDTimer = 3.0f;
        Debug.Log(currentHealth + "/" + maxHealth);
        // Get the animator controller
        animator = GetComponent<Animator>();
        //Get the audio source
        audioSource = GetComponent<AudioSource>();
    }

    //Play audio function
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Create move vector
        Vector2 move = new Vector2(horizontal, vertical);
        // If the character not ilde
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            LookDirection.Set(move.x, move.y);
            LookDirection.Normalize();
        }
        // Set vector values to determine which direction the player are looking to
        animator.SetFloat("Look X", LookDirection.x);
        animator.SetFloat("Look Y", LookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Countdown Ruby's invincible state
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0) isInvincible = false;
        }

        // Countdown Ruby's throwing skill cooldown
        if (cooldown)
        {
            UICDthrow.instance.SetValue(cooldownTimer / timeCooldown);
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0) cooldown = false;
        }
        if (!cooldown) UICDthrow.instance.SetValue(0.0f);

        // Countdown Ruby's dash cooldown
        if (dashCD)
        {
            UICDdash.instance.SetValue(dashCDTimer / 3.0f);
            dashCDTimer -= Time.deltaTime;
            if (dashCDTimer < 0) 
            {
                dashCD = false;
                dashCDTimer = 3.0f;
            }
        }
        if (!dashCD) UICDdash.instance.SetValue(0.0f);

        // Press C to fire the projectile
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        // Press V to dash
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (dashCD) return;
            else
            {
                dash = 0.4f;
                dashCD = true;
            }
        }

        // Ruby will dash for a 0.1s duration after V button is pressed
        if (dash > 0)
        {
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                dash = 0.0f;
                delay = 0.1f;
            }
        }
        // Raycast when press X (Press X to talk with NPC)
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, LookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }

        // If player health = 0 then game Over
        if (currentHealth == 0)
        {
            gameover -= Time.deltaTime;
            if (gameover < 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        else return;
    }
    
    // Update movement
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime + dash * LookDirection.x;
        position.y = position.y + speed * vertical * Time.deltaTime + dash * LookDirection.y;
        rigidbody2d.MovePosition(position);
        Pos = position;
    }

    // Change health function
    public void ChangeHealth(int amount)
    {
        // Apply invincible state for the set period of time after taking damage
        if (amount < 0)
        {
            if (!isInvincible)
            {
                // Trigger animation "Hit"
                animator.SetTrigger("Hit");
                //Play hit sound
                PlaySound(hitClip);

                isInvincible = true;
                invincibleTimer = timeInvincible;
                shield.Play();
            }
            else return;
        }
        if (amount > 0)
        {
            regen.Play();
        }

        // Change health value
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    // Throw projectile
    void Launch()
    {
        // Ruby's throwing skill have a cooldown in order to increase the game difficult
        if (cooldown) return;
        else
        {
            /* Create a game object variable for assigning the projectile with three parameters (The game object,
            The position of that object from player's position, The rotaion of that game object) */
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            // Quaternion.identity means "no rotation"
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            // Launch the game object
            projectile.Launch(LookDirection, 300);
            animator.SetTrigger("Launch");
            //PLay throw sound
            PlaySound(throwClip);
            cooldownTimer = timeCooldown;
            cooldown = true;
        }
    }
    // Reduce enemy count
    public void EnemyDead()
    {
        enemiesCount = enemiesCount - 1;
    }
}
