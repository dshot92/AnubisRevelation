using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSoundTrigger : MonoBehaviour
{

    public AudioSource boss_music;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            boss_music.Play();
        }
    }
}
