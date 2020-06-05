using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strong_Controller : MonoBehaviour
{
    public float item_rotating_speed = 2f;
    public int force_multiplier;
    public float force_seconds;
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
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(Player_IncreaseMeele(player));
            StartCoroutine(PlaySound());
        }
    }

    public IEnumerator PlaySound()
    {
        audio.Play();

        yield return new WaitForSeconds(audio.clip.length);
    }
    public IEnumerator Player_IncreaseMeele(PlayerController p)
    {
        p.meele_power += force_multiplier;

        yield return new WaitForSeconds(force_seconds);

        p.meele_power -= force_multiplier;
        //Destroy(gameObject);
    }
}
