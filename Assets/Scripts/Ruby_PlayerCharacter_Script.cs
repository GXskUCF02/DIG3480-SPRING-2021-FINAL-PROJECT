using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ruby_PlayerCharacter_Script : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public int health { get { return currentHealth; }}
    int currentHealth;

    public GameObject projectilePrefab;

    public GameObject healthUpPrefab;

    public GameObject damagePrefab;
    
    public float timeInvincible = 8.0f;
    bool isInvincible;
    float invincibleTimer;
    
    Rigidbody2D rigidbody2d;
    float horizontal; 
    float vertical;
    
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    AudioSource audioSource;

    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    public Text scoreText;
    public Text winText;
    public Text loseText;
    public Text gameOverText;
    public Text cogAmmoText;
    
    private int score;
    private int scoreValue = 0;
    
    private int cogAmmo;

    private Rigidbody2D rd2d;

    public bool gameOver = false;

    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        
        audioSource= GetComponent<AudioSource>();

        scoreText.text = scoreValue.ToString();
        score = 0;
        SetScoreText();

        cogAmmo = 4;
        SetCogAmmoText();

        winText.text = " ";
        loseText.text = " ";
        gameOverText.text = " ";

        

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (cogAmmo >= 1)
            {
                Launch();
                cogAmmo = cogAmmo - 1; 
                SetCogAmmoText();
            }
            else if (cogAmmo == 0)
            {

            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPC_Script character = hit.collider.GetComponent<NPC_Script>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (Input.GetKey(KeyCode.R))

        {

            if (gameOver == true)

            {

              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;

            Instantiate(damagePrefab, transform.position, Quaternion.identity);
            
            PlaySound(hitSound);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        
        if (currentHealth == 0)
        {
            speed = 0.0f;
            animator.SetTrigger("Hit");
            loseText.text = "You lost the game! Game made by Gage Schroeder";
            gameOverText.text = "Press R to restart the game at the current level.";
        }
    }


    public void ChangeScore(int score)
    {   
        

    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        CogProjectile projectile = projectileObject.GetComponent<CogProjectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwSound);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void SetScoreText()
    {
        scoreText.text = "Number of Robots Fixed: " + score.ToString();
        
        if (score == 4)
        {
            winText.text = "You fixed all the robots! Press R to restart the game! Game made by Gage Schroeder";
        }
        
    }

    void SetCogAmmoText()
    {
        cogAmmoText.text = "Amount of Cogs Left: " + cogAmmo.ToString();

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Cog Pickup")
        {
            cogAmmo = cogAmmo + 4;
            SetCogAmmoText();
            Destroy(collision.collider.gameObject);
        }
    }
}
