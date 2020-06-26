using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Speed_Controller : MonoBehaviour
{
    public float item_rotating_speed = 2f;
    public float speed_seconds;
    public float speed_multiplier;
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
            // Idea for Coroutine implementation for speeding up player
            // https://stackoverflow.com/questions/57929638/action-for-a-period-of-time-unity
            StartCoroutine(Player_speedup(other.gameObject));
            StartCoroutine(PlaySound());
        }
    }

    public IEnumerator PlaySound()
    {
        audio.Play();
        
        yield return new WaitForSeconds(audio.clip.length);
        StartCoroutine(Destroy());
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(speed_seconds + 2 );
        Destroy(gameObject);
    }

    public IEnumerator Player_speedup(GameObject player)
    {
        FirstPersonController p = player.GetComponent<FirstPersonController>();
        p.m_RunSpeed = p.m_RunSpeed_BASELINE * speed_multiplier;
        p.m_WalkSpeed = p.m_WalkSpeed_BASELINE * speed_multiplier;
        
        yield return new WaitForSeconds(speed_seconds);

        p.m_RunSpeed = p.m_RunSpeed_BASELINE; 
        p.m_WalkSpeed = p.m_WalkSpeed_BASELINE;
    }

}
