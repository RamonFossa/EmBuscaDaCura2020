using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float side;
    public float speedBullet;
    public GameObject spray;
    Player player;
   
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       
        if (player.facingRight)
        {
            side = 1f; 
        } else { side = -1f; }

        Destroy(gameObject, 2);

    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector2.right * side * speedBullet * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        Destroy(spray);
    }

}
