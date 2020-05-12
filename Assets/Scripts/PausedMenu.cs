using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenu : MonoBehaviour
{
    public Slider volume_slider;
    public AudioSource audioSource;

    public GameObject pauseCanvas;
    public GameObject optionMenu;

    private void Awake()
    {
        audioSource = GameObject.FindObjectOfType<AudioSource>();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        audioSource.UnPause();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        audioSource.Pause();
        pauseCanvas.SetActive(true);
    }

    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void VolumeUpdate()
    {
        audioSource.volume = volume_slider.value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Resume();
                pauseCanvas.SetActive(false);
            }
            else
            {
                Pause();
            }
        }
    }
}
