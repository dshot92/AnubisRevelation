using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)


	public static GameManager instance = null;
	public static PlayerController player;
	public float pause_time = 3f;

	private void OnEnable()
	{
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
		SceneManager.LoadScene(scene);
	}
	private void Update()
	{
		if (SceneManager.GetActiveScene().name.Equals("Level3"))
		{
			RenderSettings.fog = false;
		}
		else
		{
			RenderSettings.fog = true;
			//RenderSettings.fogColor = new Color(255, 143, 49);
		}

		if (player == null)
		{
			player = GameObject.FindObjectOfType<PlayerController>();
		}

		
		if(player.life <= 0)
		{
			Debug.Log("GameOver");
			Invoke("GameOver", pause_time);
			player.life = player.max_life;
		}

	}


	void GameOver()
	{
		SceneManager.LoadScene("MainMenu");
	}
}

