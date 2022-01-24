using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject principalHUD;
    public GameObject settings;
    public AudioMixer audioMixer;
    public Slider masterVol, musicVol, sfxVol;
    

    void Update()
    {
        if(Input.GetButtonDown("Pause") && isPaused && SceneManager.GetActiveScene().buildIndex == 1 && Cutscene1.canPause)
        {
            Resume();  
        }else if(Input.GetButtonDown("Pause") && !isPaused && SceneManager.GetActiveScene().buildIndex == 1 && Cutscene1.canPause)
        {
            Pause();
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        principalHUD.SetActive(true);
        settings.SetActive(false);
    }
 
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        principalHUD.SetActive(false);
    }

    public void LoadMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LoadSettings()
    {
        pauseMenuUI.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Back()
    {
        pauseMenuUI.SetActive(true);
        settings.SetActive(false);
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
}
