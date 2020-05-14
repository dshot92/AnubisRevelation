using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemRotator : MonoBehaviour
{
    public float item_rotating_speed = 2f;

    // Creato just one script to handle multiples (for this case only 3, that's why i'm doing it, objects type and characteristics

    //Assign type of item from inspector and everything is handles in the trigger collision
    [Header("Heart:")]
    public bool is_heart = false;
    public int healt_increase = 1;

    [Space(1)]
    [Header("Amulet:")]
    public bool is_amulet = false;

    [Space(1)]
    [Header("")]
    public bool is_sword = false;
    
    [Space(1)]
    [Header("")]
    public bool is_flash = false;
    public float speed_seconds;
    public float speed_multiplier;

    [Space(1)]
    [Header("Force")]
    public bool is_force = false;
    public int force_multiplier;
    public float force_seconds;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(Vector3.up, item_rotating_speed * Time.deltaTime);
        //transform.Rotate(Vector3.up, r_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();
            if (is_heart)
            {
                player.life += healt_increase;
                player.life %= player.max_life;
                Destroy(gameObject);
            }

            if (is_sword)
            { 
                player.sword.SetActive(true);
                Destroy(gameObject);
            }

            if (is_flash)
            {
                // Idea for Coroutine implementation for speeding up player
                // https://stackoverflow.com/questions/57929638/action-for-a-period-of-time-unity
                StartCoroutine(Player_speedup(other.gameObject));

            }

            if (is_amulet)
            {
                GameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (is_force)
            {
                StartCoroutine(Player_IncreaseMeele(player));
            }

            // If gameobject is destroye no coroutine can be done!!!!!!
            // Destroy it inside the coroutine
            
        }
    }

    public IEnumerator Player_IncreaseMeele(PlayerController p)
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
   
        p.meele_power += force_multiplier;


        yield return new WaitForSeconds(force_seconds);

        p.meele_power -= force_multiplier;

        Destroy(gameObject);
    }

    public IEnumerator Player_speedup(GameObject player)
    {
        FirstPersonController p = player.GetComponent<FirstPersonController>();
        
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
       
        p.m_RunSpeed *= speed_multiplier;
        p.m_WalkSpeed *= speed_multiplier;


        yield return new WaitForSeconds(speed_seconds);

        p.m_RunSpeed /= speed_multiplier;
        p.m_WalkSpeed /= speed_multiplier;

        Destroy(gameObject);
    }
}
