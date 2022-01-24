using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tatu : MonoBehaviour
{
    public Animator anim;
    public Player player;
    public GameObject all;
    public Rigidbody2D rb;
    public Transform spawn;
    public GameObject deadParticle;
    public CapsuleCollider2D colliderT;
    public float speed;
    public int life;
    public bool spawnArea;
    private bool f;
    private bool dead;
    private bool inSpawn;
    private bool playerDetected;
    private bool lookingPlayerLeft;
    private bool facingLeft = true;
    private Transform target;

    void Start()
    {
        deadParticle.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerDetected = false;
        inSpawn = true;
        dead = false;
        life = 3;
        lookingPlayerLeft = true;
        f = true;
    }


    void Update()
    {
        if (!dead)
        {
            FollowPlayer();
            SpawnVerify();
            Direction();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (life > 1)
            {
                life--;
            }
            else if (life == 1)
            {
                life--;
                Die();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Granada"))
        {
            if (life <= 4)
            {
                life = life - life;
                Die();
            }
        }
    }


    void Die()
    {
        deadParticle.SetActive(true);
        dead = true;
        anim.SetBool("Dead", true);
        Destroy(all, 4);
        colliderT.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Static;

    }
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void FollowPlayer()
    {
        if (spawnArea && player.alive)
        {
            if (!f)
            {
                Flip();
                f = true;
            }

            inSpawn = false;
            anim.SetBool("PlayerDetected", true);
            playerDetected = true;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (!spawnArea && !inSpawn || player.alive == false)
        {
            if (f)
            {
                Flip();
                f = false;
            }
            playerDetected = false;
            transform.position = Vector2.MoveTowards(transform.position, spawn.position, speed * Time.deltaTime);

        }
    }
    void SpawnVerify()
    {
        if (transform.position.x == spawn.position.x && !playerDetected && !f)
        {
            Flip();
            inSpawn = true;
            anim.SetBool("PlayerDetected", false);
            f = true;
        }
    }

    void Direction()
    {
        if (target.position.x < transform.position.x && !lookingPlayerLeft && playerDetected)
        {
            Flip();
            lookingPlayerLeft = true;
        }
        else if (target.position.x > transform.position.x && lookingPlayerLeft && playerDetected)
        {
            Flip();
            lookingPlayerLeft = false;
        }
    }

}