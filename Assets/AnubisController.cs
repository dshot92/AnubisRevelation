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
    public TextMeshProUGUI life_text;

    Animator anim;
    public AudioSource die_voice;
    public AudioSource hit_sound;
    public AudioSource punch_sound;

    public int life = 10;

    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    PlayerController play_contr;

    float attack_cooldown = (3.533f * 0.5f); //attack animation time duration
    public float teleport_cooldown = 15f;
    float elapsed_time = 0f;
    float elapsed_time_tp = 0f;

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
        elapsed_time_tp += Time.deltaTime;

        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);
        if (distancePlayer < meele_radius && elapsed_time > attack_cooldown)
        {
            agent.SetDestination(player.transform.position);
            InstantlyTurnAttack();
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);

            life_text.gameObject.SetActive(true);

            Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red, 100000f);
            if (elapsed_time > attack_cooldown)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, meele_radius))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        StartCoroutine(Stop_while_attack());
                    }
                }
            }
        }
        else if (distancePlayer < awareness_radius && distancePlayer > meele_radius)
        { 
            agent.SetDestination(player.transform.position);
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);

            life_text.gameObject.SetActive(false);

            agent.speed = original_speed;
            agent.speed *= speed_multiplier;

     
        }
        else if (distancePlayer > awareness_radius && distancePlayer < teleport_min_distance)
        {
            agent.SetDestination(player.transform.position);
            
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", true);

            life_text.gameObject.SetActive(false);

            agent.speed = original_speed;
            agent.speed *= speed_multiplier;
        }
        else if ( distancePlayer > teleport_min_distance && distancePlayer < teleport_max_distance)
        {
            agent.SetDestination(player.transform.position);
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
        }
        else if ( life < 5 && elapsed_time_tp > teleport_cooldown)
        {
            elapsed_time_tp = 0;
            agent.SetDestination(player.transform.position);
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
        }

        InstantlyTurn(agent.destination);
        
        // uPDATE lIFE TEXT
        switch (life)
        {
            case 1:
                life_text.color = Color.red;
                break;
            case 2:
                life_text.color = Color.yellow;
                break;
        }
        life_text.text = (life.ToString());


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

    private IEnumerator Stop_while_attack()
    {
        // 3.533f =  attack time animation
        // 1.5 animation speed ->  half speed
        agent.isStopped = true;
        elapsed_time = 0f;
        anim.SetTrigger("isAttacking");
        punch_sound.Play();
        yield return new WaitForSeconds(attack_cooldown);
        play_contr.life -= meele_power;
        agent.isStopped = false;
    }
    private void InstantlyTurnAttack()
    {
        //When on target -> dont rotate!
        if ((player.transform.position - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * singleStep);
    }
}
