using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    public Slider volume_slider;
    public AudioSource[] audioSource;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void VolumeUpdate()
    {
        foreach (AudioSource sound in audioSource)
        {
            sound.volume = volume_slider.value;
        }
    }

}
