using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatoDetect : MonoBehaviour
{

    private bool spawn;
    public Rato rato;



    private void Start()
    {
        
       spawn = false;
       rato.spawnArea = false;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !spawn)
        {
            spawn = true;
           rato.spawnArea = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            spawn = false;
           rato.spawnArea = false;
        }
    }
}
