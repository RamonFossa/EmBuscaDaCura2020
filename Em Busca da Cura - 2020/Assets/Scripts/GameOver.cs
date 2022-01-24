using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text numDeaths;
    public Player player;
    public GameObject gameOver;
    public GameObject principalHUD;
    public AudioSource gameS;
    private bool doOnce;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        doOnce = true;
    }

    private void Update()
    {
        Over();
    }

    void Over()
    {
        if(player.alive == false)
        {
            principalHUD.SetActive(false);
            gameOver.SetActive(true);
            numDeaths.text = player.deaths.ToString() + " mortes";


            if (doOnce)
            {
                AudioSource gaS = Instantiate(gameS, transform.position, transform.rotation);
                gaS.Play();
                doOnce = false;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
