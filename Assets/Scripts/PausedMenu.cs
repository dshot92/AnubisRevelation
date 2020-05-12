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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.SetActive(false);
        optionMenu.SetActive(false);
        audioSource.UnPause();
        Time.timeScale = 1;

    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource.Pause();
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void MainMenuLoad()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
                //pauseCanvas.SetActive(false);
                Resume();
            }
            else
            {
                Pause();
               
            }
        }
    }
}
