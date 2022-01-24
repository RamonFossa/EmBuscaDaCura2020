using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene1 : MonoBehaviour
{
    Player player;
    public GameObject principalHUD;
    public GameObject painel1, painel2, caixa1, caixa2, caixa3, caixa4, caixa5, caixa6, caixa7, caixa8, caixa9, caixa10;
    public static bool canPause;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Destroy(gameObject);
            canPause = true;
        }
    }
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Pause();
            painel1.SetActive(true);
            painel2.SetActive(false);
            caixa1.SetActive(true);
            canPause = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);  
        }
            
    }

    public void ContinuarAA()
    {
        caixa1.SetActive(false);
        caixa2.SetActive(true);
    }

    public void ContinuarBA()
    {
        caixa2.SetActive(false);
        caixa3.SetActive(true);
    }

    public void ContinuarCA()
    {
        caixa3.SetActive(false);
        caixa4.SetActive(true);
    }

    public void ContinuarDA()
    {
        caixa4.SetActive(false);
        caixa5.SetActive(true);
        painel1.SetActive(false);
        painel2.SetActive(true);
    }

    public void VoltarAA()
    {
        caixa1.SetActive(true);
        caixa2.SetActive(false);
    }

    public void VoltarBA()
    {
        caixa2.SetActive(true);
        caixa3.SetActive(false);
    }

    public void VoltarCA()
    {
        caixa3.SetActive(true);
        caixa4.SetActive(false);
    }

    public void Continuar1()
    {
        caixa5.SetActive(false);
        caixa6.SetActive(true);
    }

    public void Continuar2()
    {
        caixa6.SetActive(false);
        caixa7.SetActive(true);
    }

    public void Continuar3()
    {
        caixa7.SetActive(false);
        caixa8.SetActive(true);
    }

    public void Continuar4()
    {
        caixa8.SetActive(false);
        caixa9.SetActive(true);
    }

    public void Continuar5()
    {
        caixa9.SetActive(false);
        caixa10.SetActive(true);
    }

    public void Continuar6()
    {
        caixa10.SetActive(false);
        painel2.SetActive(false);
        canPause = true;
        Resume();
    }

    public void Voltar1()
    {
        caixa5.SetActive(true);
        caixa6.SetActive(false);
    }

    public void Voltar2()
    {
        caixa6.SetActive(true);
        caixa7.SetActive(false);
    }

    public void Voltar3()
    {
        caixa7.SetActive(true);
        caixa8.SetActive(false);
    }

    public void Voltar4()
    {
        caixa8.SetActive(true);
        caixa9.SetActive(false);
    }

    public void Voltar5()
    {
        caixa9.SetActive(true);
        caixa10.SetActive(false);
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
        principalHUD.SetActive(true);
    }
}
