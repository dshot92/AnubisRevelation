using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Amulet_Controller : MonoBehaviour
{

    public float item_rotating_speed = 2f;
    public AudioSource audio;

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

            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(PlaySoundBeforeNextLevel());

        }
    }
    public IEnumerator PlaySoundBeforeNextLevel()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        GameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
