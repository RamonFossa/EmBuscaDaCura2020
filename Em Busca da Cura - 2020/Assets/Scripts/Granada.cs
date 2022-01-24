using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public float Speed = 4;
    public bool jogar;
    public Vector3 LaunchOffset;
    public float side;
    public CapsuleCollider2D range;
    public GameObject explosion;
    public AudioSource boo;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        range.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player.facingRight)
        {
            side = 1f;
        }
        else { side = -1f; }

        
       
            var direction = transform.right * side + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
       
        transform.Translate(LaunchOffset);
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         Invoke("Boom", Random.Range(2f, 2.5f));
    }

    void Boom()
    {
        range.enabled = true;
        Destroy(gameObject, 0.1f);
        GameObject particle = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(particle, 2);

        AudioSource booS = Instantiate(boo, transform.position, transform.rotation);
        booS.Play();
        Destroy(booS.gameObject, 1.5f);
    }
}
