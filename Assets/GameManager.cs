using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)


	public static GameManager instance = null;

	void Awake()
	{
		//if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
		
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Destroy(this);
		}

	}

	private void Update()
	{

	}
}
