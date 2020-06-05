﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)


	public static GameManager instance = null;
	public static GameObject player_object;
	public static PlayerController player;
	public float pause_time = 3f;
	bool dead = false;
	public static int player_coins = 0;

	public static int saved_times = 0;
	public static int save_coint_count = 0;
	public static string save_active_scene = "Level1";
	public static int save_healt = 10;
	public static bool has_torch = false;
	public static bool has_sword = false;
	public static bool loading = false;

	private void OnEnable()
	{
		player_object = GameObject.FindGameObjectWithTag("Player");
		player = GameObject.FindObjectOfType<PlayerController>();
	}
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(this);
		}
		player = GameObject.FindObjectOfType<PlayerController>();
	}

	void Setup()
	{
		player = GameObject.FindObjectOfType<PlayerController>();
	}

	public static void NextScene(int scene)
	{
		// Loading next scene implies game progress, don't overwrite coin count
		SceneManager.LoadScene(scene);
	}

	public static void LoadScene(string scene)
	{
		// Choosing from menu resets coins count
		SceneManager.LoadScene(scene);
		player_coins = 0;
	}

	public static void LoadSceneSaved()
	{
		// Choosing from menu resets coins count
		SceneManager.LoadScene(save_active_scene);
		loading = true;
	}
	private void Update()
	{
		_ = SceneManager.GetActiveScene().name.Equals("Level3") ? RenderSettings.fog = false : RenderSettings.fog = true;

		if (player == null) player = GameObject.FindObjectOfType<PlayerController>();

		if(player.life <= 0 && !dead)
		{
			Debug.Log("GameOver");
			Invoke("GameOver", pause_time);
			dead = true;
			player_coins = player.coins_count;
		}
	}

	public static void LoadState()
	{
		LoadSceneSaved();
	}


	void GameOver()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		player.life = player.max_life;
		dead = false;
		player.coins_count = player_coins;
	}
}

