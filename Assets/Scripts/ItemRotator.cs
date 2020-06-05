using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemRotator : MonoBehaviour
{
    public float item_rotating_speed = 2f;
    public AudioSource audio;
    // Creato just one script to handle multiples (for this case only 3, that's why i'm doing it, objects type and characteristics

    //Assign type of item from inspector and everything is handles in the trigger collision
    [Header("Healing Orb:")]
    public bool is_heart = false;
    public int healt_increase = 1;
    
    [Space(1)]
    [Header("Speed Orb")]
    public bool is_flash = false;
    public float speed_seconds;
    public float speed_multiplier; 

    [Space(1)]
    [Header("Strong Orb")]
    public bool is_force = false;
    public int force_multiplier;
    public float force_seconds;

    [Space(1)]
    [Header("Amulet:")]
    public bool is_amulet = false;

    [Space(1)]
    [Header("Sword")]
    public bool is_sword = false;

    [Space(1)]
    [Header("Torch")]
    public bool is_torch = false;

    [Space(1)]
    [Header("Jump")]
    public bool is_jump = false;
    public float jump_multiplier; 
    public float jump_seconds;

    [Space(1)]
    [Header("Coins")]
    public bool is_coin = false;
    public int coin_value;

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
            PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();
            if (is_heart)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                player.life = player.max_life;
                StartCoroutine(PlaySound());
            }

            if (is_sword)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.GetComponentInChildren<PlayerController>().meele_power++;
                player.has_sword = true;
                StartCoroutine(PlaySound());
            }

            if (is_torch)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                player.has_torch = true;
                StartCoroutine(PlaySound());
                // really bad, but this particles system remaining alive is annoying me
                gameObject.transform.position -= new Vector3(0, -50, 0);
            }

            if (is_flash)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                // Idea for Coroutine implementation for speeding up player
                // https://stackoverflow.com/questions/57929638/action-for-a-period-of-time-unity
                StartCoroutine(Player_speedup(other.gameObject));
                StartCoroutine(PlaySound());
            }

            if (is_amulet)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(PlaySoundBeforeNextLevel());
            }

            if (is_force)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(Player_IncreaseMeele(player));
                StartCoroutine(PlaySound());
            }

            if (is_jump)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                StartCoroutine(Player_IncreaseJump(other.gameObject));
                StartCoroutine(PlaySound());
            }

            if (is_coin)
            {
                gameObject.GetComponent<SphereCollider>().enabled = false;
                MeshRenderer[] rend = gameObject.GetComponents<MeshRenderer>();
                foreach (MeshRenderer r in rend) r.enabled = false;
                StartCoroutine(Player_coins_count(player));
                StartCoroutine(PlaySound());
            }

            // If gameobject is destroye no coroutine can be done!!!!!!
            // Destroy it inside the coroutine

        }
    }

    public IEnumerator Player_IncreaseMeele(PlayerController p)
    { 
        p.meele_power += force_multiplier;

        yield return new WaitForSeconds(force_seconds);

        p.meele_power -= force_multiplier;
        //Destroy(gameObject);
    }

    public IEnumerator Player_IncreaseJump(GameObject player)
    {
        FirstPersonController p = player.GetComponent<FirstPersonController>();

        p.m_JumpSpeed *= jump_multiplier;

        yield return new WaitForSeconds(jump_seconds);

        p.m_JumpSpeed /= jump_multiplier;
        //Destroy(gameObject);

    }

    public IEnumerator Player_coins_count(PlayerController p)
    {
        p.coins_count += coin_value;

        yield return new WaitForSeconds(0);
        Destroy(gameObject, audio.clip.length / 2);

    }

    public IEnumerator Player_speedup(GameObject player)
    {
        FirstPersonController p = player.GetComponent<FirstPersonController>();

        p.m_RunSpeed *= speed_multiplier;
        p.m_WalkSpeed *= speed_multiplier;

        yield return new WaitForSeconds(speed_seconds);

        p.m_RunSpeed /= speed_multiplier;
        p.m_WalkSpeed /= speed_multiplier;

    }

    public IEnumerator PlaySound()
    {

        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);

        // If gameobject is destroy, at runtime the Pause Menu cannot acces the audiosource and will not Work.
        // I^Collectables are just a few. 
        // SImply deactivate them without destroying them
        // Not clean but works

        //Destroy(gameObject);
    }

    public IEnumerator PlaySoundBeforeNextLevel()
    {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        GameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
