using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject configMenu, mainMenu;
    public AudioMixer audioMixer;
    public Text saveNot;
    public Slider masterVol, musicVol, sfxVol;
    public Dropdown dropDown;


  
    public void Continue()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            SceneManager.LoadScene(1);
        } else
        {
            saveNot.enabled = true;
        }
        
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey("x");
        PlayerPrefs.DeleteKey("y");
        PlayerPrefs.DeleteKey("Deaths");
        SceneManager.LoadScene(1);
    }

    public void Config()
    {
        configMenu.SetActive(true);
        mainMenu.SetActive(false);
        saveNot.enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        configMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetMasterVolume(float volumeMaster)
    {
        audioMixer.SetFloat("MasterVolume", volumeMaster);
        PlayerPrefs.SetFloat("MasterVolume", volumeMaster);
    }
    public void SetMusicVolume(float volumeMusic)
    {
        audioMixer.SetFloat("MusicVolume", volumeMusic);
        PlayerPrefs.SetFloat("MusicVolume", volumeMusic);
    }
    public void SetSfxVolume(float volumeSFX)
    {
        audioMixer.SetFloat("SFXVolume", volumeSFX);
        PlayerPrefs.SetFloat("SFXVolume", volumeSFX);
    }
    private void Start()
    {
        masterVol.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVol.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVol.value = PlayerPrefs.GetFloat("SFXVolume");
        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        if (PlayerPrefs.HasKey("QualityLevel"))
        {

            dropDown.value = PlayerPrefs.GetInt("QualityLevel");
        }
        
    }
    public void reset()
    {
        masterVol.value = 0;
        musicVol.value = 0;
        sfxVol.value = 0;
        audioMixer.SetFloat("MasterVolume", 0);
        audioMixer.SetFloat("MusicVolume", 0);
        audioMixer.SetFloat("SFXVolume", 0);
        PlayerPrefs.DeleteKey("SFXVolume");
        PlayerPrefs.DeleteKey("MusicVolume");
        PlayerPrefs.DeleteKey("MasterVolume");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

}
