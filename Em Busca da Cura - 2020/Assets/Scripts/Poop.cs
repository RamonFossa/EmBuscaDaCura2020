using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public GameObject particle;
    public AudioSource poo;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject poop = Instantiate(particle, transform.position, transform.rotation);
        Destroy(poop, 0.4f);
        Destroy(gameObject, 0.05f);
        AudioSource po = Instantiate(poo, transform.position, transform.rotation);
        po.Play();
        Destroy(po.gameObject, 1f);
    }
}
