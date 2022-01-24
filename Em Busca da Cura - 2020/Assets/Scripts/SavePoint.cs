using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SavePoint : MonoBehaviour
{
    Player player;
    public GameObject save, confirmation;
    private bool can;
    int quality;

    private void Awake()
    {
        save.SetActive(false);
        confirmation.SetActive(false);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Quality();
    }

    private void Update()
    {
        if (can && Input.GetKeyDown(KeyCode.E))
        {
            player.life = 3;
            player.grenades = 2;
            SaveGame();
            SceneManager.LoadScene(1);
            confirmation.SetActive(true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            save.SetActive(true);
            can = true;
        }
       
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player"))
        {
            save.SetActive(false);
            can = false;
            confirmation.SetActive(false);
        }     
    }
    void SaveGame()
    {
        PlayerPrefs.SetFloat("x", player.transform.position.x);
        PlayerPrefs.SetFloat("y", player.transform.position.y);

    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }


    void Quality()
    {
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            quality = PlayerPrefs.GetInt("QualityLevel");
            QualitySettings.SetQualityLevel(quality);
        } else
        {
            quality = 0;
            QualitySettings.SetQualityLevel(quality);
        }
    }
}