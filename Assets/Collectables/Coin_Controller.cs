using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin_Controller : MonoBehaviour
{
    public float item_rotating_speed = 2f;
    public int coin_value;
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
        PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();

        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            MeshRenderer[] rend = gameObject.GetComponents<MeshRenderer>();
            foreach (MeshRenderer r in rend) r.enabled = false;
            StartCoroutine(Player_coins_count(player));
            StartCoroutine(PlaySound());
        }
    }
    public IEnumerator Player_coins_count(PlayerController p)
    {
        p.coins_count += coin_value;
        yield return new WaitForSeconds(0);
        Destroy(gameObject, audio.clip.length / 2);
    }

    public IEnumerator PlaySound()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
    }
}
