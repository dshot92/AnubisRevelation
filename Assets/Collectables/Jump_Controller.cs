using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Jump_Controller : MonoBehaviour
{
    public float item_rotating_speed = 2f;
    public float jump_multiplier;
    public float jump_seconds;
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
            StartCoroutine(Player_IncreaseJump(other.gameObject));
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
        yield return new WaitForSeconds(jump_seconds + 2);
        Destroy(gameObject);
    }

    public IEnumerator Player_IncreaseJump(GameObject player)
    {
        FirstPersonController p = player.GetComponent<FirstPersonController>();
        p.m_JumpSpeed = p.m_JumpSpeed_BASELINE * jump_multiplier;
        yield return new WaitForSeconds(jump_seconds);
        p.m_JumpSpeed = p.m_JumpSpeed_BASELINE;
    }
}
