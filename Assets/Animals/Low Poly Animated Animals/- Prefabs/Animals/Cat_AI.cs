﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cat_AI : MonoBehaviour
{
    [Range(0f, 500f)]
    public float awareness_radius = 10f;
    public float meele_radius = 2f;
    [Range(0f, 500f)]
    public float walk_radius = 100f;
    public float singleStep = 1f;
    public float speed_multiplier = 2f;
    float original_speed;
    public int meele_power = 2;

    bool attack = false;

    public TextMeshProUGUI life_text;

    Animator anim;
    public Animation attack_anim;

    public int life = 2;

    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    PlayerController play_contr;
    AudioSource audioSource;

    float sound_cooldown = 1f;
    float attack_cooldown = 1f;
    float elapsed_time = 0f;
    private Vector3 targetDirection;

    void Start()
    {

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        original_speed = agent.speed;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        play_contr = player.GetComponentInChildren<PlayerController>();
        //Overlap step sound for 1/3 of the duration time
        sound_cooldown = audioSource.clip.length * (agent.speed / 10);
        attack_anim = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        //sound timer
        elapsed_time += Time.deltaTime;
        anim.SetBool("isWalking", true);

        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);

        if (distancePlayer < meele_radius)
        {
            //anim.SetTrigger("attacking");
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);

            anim.Play("Cat_Attack");
            
            if (elapsed_time > attack_cooldown)
            {
                elapsed_time = 0f;
                play_contr.life -= meele_power;
            }
            life_text.gameObject.SetActive(true);
        }
        else if (distancePlayer < awareness_radius)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", true);


            agent.speed = original_speed;
            agent.speed *= speed_multiplier;

            life_text.gameObject.SetActive(false);
            life_text.gameObject.SetActive(false);
            //walk torwards player
            agent.SetDestination(player.transform.position);
        }
        else if (distancePlayer > awareness_radius)
        {
            //Random walk
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);


            agent.speed = original_speed;
            agent.speed /= speed_multiplier;

            life_text.gameObject.SetActive(false);


            /// https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html

            // If 1/5 of destination left rework another random one
            ///TODO
            // life value could act as a swiftness multiplier, creating a more chaotically pattern based on remaining lifes points
            if (agent.remainingDistance < walk_radius / 5)
            {
                Vector3 randomDirection = Random.insideUnitSphere * walk_radius;
                randomDirection += transform.position;
                UnityEngine.AI.NavMeshHit hit;
                Vector3 finalPosition = Vector3.zero;
                if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, walk_radius, 1))
                {
                    finalPosition = hit.position;
                }
                agent.SetDestination(finalPosition);
            }
        }

        InstantlyTurn(agent.destination);

        //Debug.Log(sound_cooldown.ToString());
        if (elapsed_time > sound_cooldown && !attack)
        {
            elapsed_time = 0f;
            audioSource.volume = (1 / distancePlayer / distancePlayer);  // Inverse square law
            audioSource.Play();
        }

        // uPDATE lIFE TEXT
        life_text.text = (life.ToString());

        if (life <= 0)
        {
            Destroy(gameObject);
        }
        attack = false;
    }

    // https://answers.unity.com/questions/1170087/instantly-turn-with-nav-mesh-agent.html
    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * singleStep);
    }
}
