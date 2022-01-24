using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TatuDetect : MonoBehaviour
{
    private bool spawn;
    public Tatu tatu;



    private void Start()
    {

        spawn = false;
        tatu.spawnArea = false;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !spawn)
        {
            spawn = true;
            tatu.spawnArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawn = false;
            tatu.spawnArea = false;
        }
    }
}