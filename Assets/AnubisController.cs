using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq.Expressions;

public class AnubisController : MonoBehaviour
{
    [Range(0f, 100f)]
    public float awareness_radius = 10f;
    [Range(0f, 100f)]
    public float teleport_min_distance = 20f;
    [Range(0f, 100f)]
    public float teleport_max_distance = 50f;
    public float meele_radius;
    public float singleStep = 1f;
    public float speed_multiplier = 2f;
    float original_speed;
    public int meele_power = 2;
    public float tp_distance = 7f;

    bool attack = false;

    Animator anim;

    public AudioSource die_voice;
    public AudioSource hit_sound;

    public int life = 10;

    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    PlayerController play_contr;

    float attack_cooldown = 3.32f / 1.5f; //attack animation time duration
    float elapsed_time = 0f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        original_speed = agent.speed;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        play_contr = player.GetComponentInChildren<PlayerController>();
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
    }

    void FixedUpdate()
    {
        //sound timer
        elapsed_time += Time.deltaTime;

        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);
        Debug.Log(distancePlayer);
        if (distancePlayer < meele_radius)
        {
            //anim.SetTrigger("attacking");
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);

            agent.isStopped = true;
            if (elapsed_time > attack_cooldown)
            {
                anim.SetTrigger("isAttacking");
                elapsed_time = 0f;
                play_contr.life -= meele_power;
            }
            agent.isStopped = false;
        }
        else if (distancePlayer < awareness_radius && distancePlayer > meele_radius)
        { 
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);

            agent.speed = original_speed;
            agent.speed *= speed_multiplier;

            agent.SetDestination(player.transform.position);
     
        }
        else if (distancePlayer > awareness_radius && distancePlayer < teleport_min_distance)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", true);

            agent.speed = original_speed;
            agent.speed *= speed_multiplier;

            agent.SetDestination(player.transform.position);
        }
        else if (life < 4 &&  distancePlayer > teleport_min_distance && distancePlayer < teleport_max_distance)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            agent.isStopped = true;
            agent.transform.position = player.transform.position;
            agent.transform.rotation = player.transform.rotation;
            agent.transform.position += (-player.transform.forward) * -tp_distance;
            agent.transform.Rotate(new Vector3(0, 180, 0));
            agent.isStopped = false;

            die_voice.volume = 10 / distancePlayer;
            die_voice.Play();
            Debug.Log("Teleporting");
            agent.SetDestination(player.transform.position);
        }

        InstantlyTurn(agent.destination);

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void InstantlyTurn(Vector3 destination)
    {
        if ((destination - transform.position).magnitude < 0.1f) return;
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation,qDir, Time.deltaTime * singleStep);
    }
}
