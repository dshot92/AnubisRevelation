using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Audio;

public class MainMenuController : MonoBehaviour
{
    public Slider volume_slider;
    public AudioSource[] audioSource;

    private void Awake()
    {
        RenderSettings.fog = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Setup()
    {
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
        volume_slider.value = GameManager.volume_slider;
    }
    public void Level1()
    {
        GameManager.LoadScene("Level1");
        GameManager.has_sword = false;
        GameManager.has_torch = false;
        GameManager.player_coins = 0;

    }
    public void Level2()
    {
        GameManager.LoadScene("Level2");
        GameManager.has_sword = false;
        GameManager.has_torch = false;
        GameManager.player_coins = 0;
    }
    public void Level3()
    {
        GameManager.LoadScene("Level3");
        GameManager.has_sword = false;
        GameManager.has_torch = false;
        GameManager.player_coins = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Load()
    {
        if ( GameManager.saved_times > 0) GameManager.LoadSceneSaved();
    }

    public void VolumeUpdate()
    {
        GameManager.volume_slider = volume_slider.value;
    }
}
