using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomba : MonoBehaviour
{
    public float velocity;
    private float side;
    private bool facingLeft;
    public Transform targetP;
    public GameObject poopP;
    public float cooldown;
    private float nextPoop;
    Player player;
    public bool variant;
    public float temporizador;
    private float t;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        velocity = 5.5f;
        t = 0;
        if (variant)
        {
            Flip();
            side = 1;
            facingLeft = true;
        } else
        {
            side = -1;
            facingLeft = true;  
        }    
    }

    private void Update()
    {
        Behaviour();
        Poop();
        Timer();
    }

       void Timer()
    {
        if(Time.time > t)
        {
            side *= -1;
            Flip();
            t = Time.time + temporizador;
        }  
    }
           
        
    void Flip()
    {
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void Behaviour()
    {
        transform.Translate(Vector2.right * side * velocity * Time.deltaTime);
    }

    void Poop()
    {
        
        if (Time.time > nextPoop && Vector2.Distance(transform.position, player.transform.position) < 12)
        {
            GameObject poop = Instantiate(poopP, targetP.position, targetP.rotation);
            nextPoop = Time.time + cooldown;
            Destroy(poop, 4);
        }
       
    }
}
