using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenu : MonoBehaviour
{
    public Slider volume_slider;
    public AudioSource[] audioSource;

    public GameObject pauseCanvas;
    public GameObject optionMenu;
    PlayerController player;
    public GameObject gray_back;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        gray_back.SetActive(false);

        foreach (AudioSource sound in audioSource)
        {
            sound.UnPause();
        }
        Time.timeScale = 1;

    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gray_back.SetActive(true);


        audioSource = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in audioSource)
        {
            sound.Pause();
        }
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Save()
    {
        GameManager.saved_times++;
        GameManager.save_coint_count = player.coins_count;
        GameManager.save_healt = player.life;
        GameManager.save_active_scene = SceneManager.GetActiveScene().name.ToString();
        GameManager.has_sword = player.has_sword;
        GameManager.has_torch = player.has_torch;
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        Time.timeScale = 1;
        foreach (AudioSource sound in audioSource)
        {
            sound.UnPause();
        }
    }

    public void Load()
    {
        GameManager.LoadState();
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        Time.timeScale = 1;
        foreach (AudioSource sound in audioSource)
        {
            sound.UnPause();
        }
    }

    public void MainMenuLoad()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void VolumeUpdate()
    {
        foreach(AudioSource sound in audioSource)
        {
            sound.volume = volume_slider.value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (Time.timeScale == 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                Cursor.visible = !Cursor.visible;
                Cursor.lockState = CursorLockMode.None;
                Pause();
            }
        }
    }
}
