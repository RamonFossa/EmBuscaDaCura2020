using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene2 : MonoBehaviour
{
    Player player;
    public GameObject principalHUD;
    public GameObject painel1, caixa1, caixa2, caixa3, caixa4;
    public static bool canPause;
    public Transform position;
    public GameObject explosion;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.alive)
        {
            Pause();
            painel1.SetActive(true);
            caixa1.SetActive(true);
            canPause = false;
            player.rbP.bodyType = RigidbodyType2D.Static;
            player.rend.enabled = false;
            Destroy(player.staminaBackground);
            Destroy(player.stamina);
            player.gameObject.transform.position = position.position;
        }
    }

    

    public void ContinuarA()
    {
        caixa1.SetActive(false);
        caixa2.SetActive(true);
       
        
        
    }

    public void ContinuarB()
    {
        caixa2.SetActive(false);
        caixa3.SetActive(true);
    }

    public void ContinuarC()
    {
        caixa3.SetActive(false);
        caixa4.SetActive(true);
    }

    public void VoltarA()
    {
        caixa1.SetActive(true);
        caixa2.SetActive(false);
    }

    public void VoltarB()
    {
        caixa2.SetActive(true);
        caixa3.SetActive(false);
    }

    public void Menu()
    {
        
        Resume();
        canPause = true;
        SceneManager.LoadScene(0);
    }

    void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.isPaused = true;
        principalHUD.SetActive(false); 
    }

    void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.isPaused = false;
    }
}
