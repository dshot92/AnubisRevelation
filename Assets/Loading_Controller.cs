using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_Controller : MonoBehaviour
{
	static float elapsed_time = 0f;

	void Start()
    {
		//StartCoroutine(LoadAsyncOperation());
    }

	private void Update()
	{
		elapsed_time += Time.deltaTime;
		if (elapsed_time > 5f)
		{
			SceneManager.LoadScene(GameManager.scene_to_load);
		}
	}

	public static IEnumerator LoadAsyncOperation()
	{
		AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(GameManager.scene_to_load);
		if (elapsed_time > 3.5f)
		{
			while (sceneLoad.progress < 1)
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
