using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Amulet_Controller : MonoBehaviour
{

    public float item_rotating_speed = 2f;
    public AudioSource audio;
    public bool final_amulet = false;

    private void Setup()
    {
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        transform.RotateAround(Vector3.up, item_rotating_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.player_coins = other.gameObject.GetComponentInChildren<PlayerController>().coins_count;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (final_amulet) StartCoroutine(PlaySoundBeforeEndScreen());
            else StartCoroutine(PlaySoundBeforeNextLevel());
        }
    }
    public IEnumerator PlaySoundBeforeNextLevel()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        GameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public IEnumerator PlaySoundBeforeEndScreen()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        SceneManager.LoadScene("EndScene");
    }
}
