 using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour{
    Vector2 Pos;
    public float accelerationP;
    public float jumpForce;
    public float dashForce;
    public float dashCooldown;
    private float dashTimer;
    public bool facingRight;
    private bool onWater;
    public bool isGrounded;
    private int canJump;
    public int jumpValue;
    public float jumpInWater;
    public Rigidbody2D rbP;
    public Animator anim;
    private float currentStamina;
    public GameObject staminaBackground;
    public GameObject stamina;
    public int selectedWeapon;
    public int life = 3;
    public int grenades = 2;
    public GameObject shoot;
    public GameObject granada;
    public Transform r_shootSpawn;
    public Transform l_shootSpawn;
    public Transform r_granadaSpawn;
    public Transform l_granadaSpawn;
    public float fireCooldown;
    private float nextFire = 0;
    public bool alive;
    private float damageCooldown, damageTime;
    public AudioSource damage, fire, dash;
    public Renderer rend;
    public int deaths;
    public GameObject trail;
    
    // Start is called before the first frame update
  
    void Start(){
        trail.SetActive(false);
        onWater = false;
        rbP = GetComponent<Rigidbody2D>();
        facingRight = true;
        dashTimer = 0f;
        currentStamina = 100;
        jumpValue = 1;
        canJump = jumpValue;
        selectedWeapon = 1;
        alive = true;
        damageCooldown = 0.5f;
        Spawn();
        DeathCount();
        anim.SetBool("Alive", true);
        anim.SetBool("IsGrounded", true);
    }//Start END

    // Update is called once per frame
    void Update(){
        if (alive)
        {
            if (isGrounded)
            {
                anim.SetBool("IsGrounded", true);
            }
            
            


            Movement();
            Jump();
            Dash();
            Weapons();
            CheckVelocity();
        }
    }//Update END

         
            

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = jumpForce - jumpInWater;
            onWater = true;
        }
        
        if (collision.gameObject.CompareTag("Void"))
        {
            life = 0;
            alive = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Water"))
        {
            jumpForce = jumpForce + jumpInWater;
            onWater = false;    
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("IsGrounded", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && Time.time > damageTime)
        {
            ReceiveDamage();
            damageTime = Time.time + damageCooldown;


            if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rbP.velocity = Vector2.right * 10f;
            } else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rbP.velocity = Vector2.right * -10f;
            } 
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPoop"))
        {
            ReceiveDamage();
        }
       
    }


    void Spawn()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Pos = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
            transform.position = Pos;
        }
        if(SceneManager.GetActiveScene().name == "Scene01" && !PlayerPrefs.HasKey("x") && !PlayerPrefs.HasKey("y"))
        {
            Pos = new Vector2(-114.18f, 4.98f);
            transform.position = Pos;
        }
        if (SceneManager.GetActiveScene().name == "Tests")
        {
            Pos = new Vector2(-4.49f, -3.24f);
            transform.position = Pos;
        }
    }

    void Movement(){
        float direction = Input.GetAxis("Horizontal");
        if(direction != 0 && !onWater)
        {
           transform.Translate(Vector2.right * Time.deltaTime * accelerationP * direction);
           
        }
        if (direction != 0 && onWater)
        {
           transform.Translate(Vector2.right * Time.deltaTime * accelerationP * direction / 3f);
        }




        //Player horizontal translation
        //=======================================================

        //Right or Left

        if (direction > 0 && !facingRight)
        {
            Flip();
            facingRight = true;
        }
        else if (direction < 0 && facingRight)
        {
            Flip();
            facingRight = false;
        }
        
    }//Movement END


    void Jump(){
        
        if (isGrounded){
            canJump = jumpValue;
          
        }

        if (Input.GetButtonDown("Jump") && canJump > 0 && PauseMenu.isPaused == false)
        {
            rbP.velocity = Vector2.up * jumpForce;
            canJump--;
        }

    }//Jump END
 
    void Dash(){
        Stamina();
        if (Input.GetButtonDown("Dash") && facingRight && Time.time > dashTimer && !onWater && PauseMenu.isPaused == false)
        {
            rbP.AddForce(new Vector2(dashForce * 1, 0f), ForceMode2D.Impulse);
            dashTimer = dashCooldown + Time.time;

            AudioSource daS = Instantiate(dash, transform.position, transform.rotation);
            daS.Play();
            Destroy(daS.gameObject, 1.5f);

            
            trail.SetActive(true);
            Invoke("TrailDesactive", 0.5f);

        } else if (Input.GetButtonDown("Dash") && !facingRight && Time.time > dashTimer && !onWater && PauseMenu.isPaused == false)
        {
            rbP.AddForce(new Vector2(dashForce * -1, 0f), ForceMode2D.Impulse);
            dashTimer = dashCooldown + Time.time;

            AudioSource daS = Instantiate(dash, transform.position, transform.rotation);
            daS.Play();
            Destroy(daS.gameObject, 1.5f);

          
            trail.SetActive(true);
            Invoke("TrailDesactive", 0.5f);


        }
    } //Dash END

    void Stamina(){
        stamina.transform.localScale = new Vector3(currentStamina / 100, 1, 1);
        if (currentStamina == 100 && Input.GetButtonDown("Dash") && PauseMenu.isPaused == false)
        {
            currentStamina = 0;
        } else if(currentStamina == 0 && Time.time > dashTimer && PauseMenu.isPaused == false)
        {
            currentStamina = 100;
        }
    }//Stamina END

    void Weapons()
    {
        if (Input.GetButtonDown("Weapon1") && PauseMenu.isPaused == false)
        {
            selectedWeapon = 1;
        } else if (Input.GetButtonDown("Weapon2") && PauseMenu.isPaused == false)
        {
            selectedWeapon = 2;
        }
        switch (selectedWeapon)
        {
            case 1:
                
                if (Input.GetButtonDown("Fire") && facingRight && Time.time > nextFire && PauseMenu.isPaused == false) {
                    GameObject cloneShoot = Instantiate(shoot, r_shootSpawn.position, r_shootSpawn.rotation);
                    nextFire = Time.time + fireCooldown;
                    AudioSource soundF = Instantiate(fire, transform.position, transform.rotation);
                    Destroy(soundF.gameObject, 1f);
                } else if (Input.GetButtonDown("Fire") && !facingRight && Time.time > nextFire && PauseMenu.isPaused == false) {
                    GameObject cloneShoot = Instantiate(shoot, l_shootSpawn.position, l_shootSpawn.rotation);
                    nextFire = Time.time + fireCooldown;
                    AudioSource soundF = Instantiate(fire, transform.position, transform.rotation);
                    Destroy(soundF.gameObject, 1f);
                }


                break;

            case 2:
                if (Input.GetButtonDown("Fire") && facingRight && Time.time > nextFire && PauseMenu.isPaused == false && grenades > 0) {
                    GameObject cloneGranada = Instantiate(granada, r_granadaSpawn.position, r_granadaSpawn.rotation);
                    nextFire = Time.time + fireCooldown;
                    grenades--;
                } else if (Input.GetButtonDown("Fire") && !facingRight && Time.time > nextFire && PauseMenu.isPaused == false && grenades > 0) {
                    GameObject cloneGranada = Instantiate(granada, l_granadaSpawn.position, l_granadaSpawn.rotation);
                    nextFire = Time.time + fireCooldown;
                    grenades--;
                }

                break;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

   void ReceiveDamage()
    {
        if (life > 1)
        {
            life--;
            AudioSource sound = Instantiate(damage, transform.position, transform.rotation);
            sound.Play();
            Destroy(sound.gameObject, 1.5f);
        } else if(life == 1)
        {
            life--;
            alive = false;
            anim.SetBool("Alive", false);
            deaths++;
            PlayerPrefs.SetInt("Deaths", deaths);
        }
       
    }

    void CheckVelocity() 
    { 
    if(Input.GetAxis("Horizontal") != 0)
        {
            anim.SetFloat("Velocity", 5);
        } else { anim.SetFloat("Velocity", -5); }
    
    }

    void DeathCount()
    {
        if (PlayerPrefs.HasKey("Deaths")) {
            deaths = PlayerPrefs.GetInt("Deaths");
        } else {
            deaths = 0;
        }
    } 
   
    void TrailDesactive()
    {
       
        trail.SetActive(false);
    }
    
}