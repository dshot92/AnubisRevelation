using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class SnakeController : MonoBehaviour
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

    public int life = 2;

    GameObject player;
    UnityEngine.AI.NavMeshAgent agent;
    PlayerController play_contr;
    public AudioSource snake_iss;
    public AudioSource snake_bite;

    float sound_cooldown = 1f;
    float attack_cooldown = 1f;
    float elapsed_time = 0f;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        original_speed = agent.speed;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        play_contr = player.GetComponentInChildren<PlayerController>();
        //Overlap step sound for 1/3 of the duration time
        sound_cooldown = snake_iss.clip.length * (agent.speed / 5);
    }

    void FixedUpdate()
    {
        //sound timer
        elapsed_time += Time.deltaTime;
        anim.SetBool("isWalking", true);

        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);
        //calculate distance e direction to player.
        if (!play_contr.has_torch)
        {
            if (distancePlayer < meele_radius * 2)
            {
                life_text.gameObject.SetActive(true);
            }
            else
            {
                life_text.gameObject.SetActive(false);
                life_text.gameObject.SetActive(false);
            }

            if (distancePlayer > awareness_radius)
            {
                //Random walk
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);

                agent.speed = original_speed;
                agent.speed /= speed_multiplier;

                if (agent.remainingDistance < walk_radius / 5)
                {
                    Vector3 randomDirection = Random.insideUnitSphere * walk_radius;
                    randomDirection += transform.position;
                    NavMeshHit hit;
                    Vector3 finalPosition = Vector3.zero;
                    if (NavMesh.SamplePosition(randomDirection, out hit, walk_radius, 1))
                    {
                        finalPosition = hit.position;
                    }
                    agent.SetDestination(finalPosition);
                }
            }
            else if (distancePlayer < awareness_radius && distancePlayer > meele_radius)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", true);

                agent.speed = original_speed;
                agent.speed *= speed_multiplier;

                agent.SetDestination(player.transform.position);
            }
            else if (distancePlayer < meele_radius)
            {
                InstantlyTurnAttack();

                agent.SetDestination(player.transform.position);
                if (elapsed_time > attack_cooldown)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, meele_radius))
                    {
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            anim.SetBool("isWalking", false);
                            anim.SetBool("isRunning", false);
                            InstantlyTurnAttack();
                            Debug.Log("Player hitted");
                            snake_bite.Play();
                            anim.Play("Snake_Attack");
                            play_contr.life -= meele_power;
                            elapsed_time = 0f;
                        }
                    }
                }
            }
            
        }
        else
        {
            // Run away from player with torch!!
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", true);

            Vector3 finalPosition = transform.position - player.transform.position;
            agent.SetDestination(finalPosition);


            agent.speed = original_speed;
            agent.speed *= speed_multiplier;
        }

        InstantlyTurn(agent.destination);

        //Debug.Log(sound_cooldown.ToString());
        if (elapsed_time > sound_cooldown )
        {
            elapsed_time = 0f;
            snake_iss.volume = (1 / distancePlayer / distancePlayer);  // Inverse square law
            snake_iss.Play();
        }

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
    private void InstantlyTurnAttack()
    {
        //When on target -> dont rotate!
        if ((player.transform.position - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * singleStep);
    }
}
