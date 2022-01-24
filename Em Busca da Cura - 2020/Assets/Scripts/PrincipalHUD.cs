using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrincipalHUD : MonoBehaviour
{
    public Text grenadeN, numD;
    public GameObject deathCount;
    public GameObject weapon1, weapon2;
    public GameObject life1, life2, life3;
    public Player player;
    private int life;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weapon1.SetActive(true);
        weapon2.SetActive(false);
        grenadeN.enabled = false;
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);
        Deaths();
        life = player.life;
    }

    void Update()
    {
        WeaponToggle();
        LifeToggle();
        Num();
       
    }

    void Num()
    {
        if (grenadeN.enabled)
        {
            grenadeN.text = player.grenades.ToString() + "x";
        }
    }
  void WeaponToggle()
    {
        if (Input.GetButtonDown("Weapon1"))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            grenadeN.enabled = false;
        }
        else if (Input.GetButtonDown("Weapon2"))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            grenadeN.enabled = true;
        }
    }

    void LifeToggle()
    {
        if(player.life != life)
        {
            life = player.life;

            switch (life)
            {
                case 3:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(true);
                    break;

                case 2:
                    life1.SetActive(true);
                    life2.SetActive(true);
                    life3.SetActive(false);
                    break;

                case 1:
                    life1.SetActive(true);
                    life2.SetActive(false);
                    life3.SetActive(false);
                    break;

                case 0:
                    life1.SetActive(false);
                    life2.SetActive(false);
                    life3.SetActive(false);
                    break;
            }
        } 
    }

    void Deaths()
    {
            numD.text = player.deaths.ToString() + " mortes";
    }

}