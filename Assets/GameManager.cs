using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)
	void Awake()
	{
		if (dontDestroyOnLoad) DontDestroyOnLoad(transform.gameObject);
	}

	private void Update()
	{

	}
}
