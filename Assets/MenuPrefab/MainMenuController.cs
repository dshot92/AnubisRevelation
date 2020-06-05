﻿using System.Collections;
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        audioSource = GameObject.FindObjectsOfType<AudioSource>();
    }
    public void Level1()
    {
        GameManager.LoadScene("Level1");
        GameManager.player_coins = 0;
    }
    public void Level2()
    {
        GameManager.LoadScene("Level2");
        GameManager.player_coins = 0;

    }
    public void Level3()
    {
        GameManager.LoadScene("Level3");
        GameManager.player_coins = 0;
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
