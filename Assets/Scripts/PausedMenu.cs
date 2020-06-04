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

    private void Start()
    {
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);

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

        audioSource = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource sound in audioSource)
        {
            sound.Pause();
        }
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
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
