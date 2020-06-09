using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityStandardAssets.Effects;

public class Mummy_AI : MonoBehaviour
{
    [Range(0f,500f)]
    public float awareness_radius = 10f;
    public float meele_radius = 2f;
    [Range(0f, 500f)]
    public float walk_radius = 100f;
    public float singleStep = 1f;

    bool attack = false;

    public TextMeshProUGUI life_text;

    Animator anim;

    public int life = 2;

    GameObject player;
    PlayerController play_contr;
    NavMeshAgent agent;
    public AudioSource footsteps;
    public AudioSource hit_sound;

    float sound_cooldown = 1f;
    public float attack_cooldown = 1f;
    float elapsed_time = 0f;

    public GameObject smoke;
    public int meele_power = 1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        play_contr = player.GetComponentInChildren<PlayerController>();
        //Overlap step sound for 1/3 of the duration time
        sound_cooldown = footsteps.clip.length * (agent.speed/10);
    }

    void FixedUpdate()
    {
        //Debug.Log(agent.destination);

        // 1) Can i hit player?
        // 2) walk to player
        // 3) if no player in sight, random walk

        //sound timer
        elapsed_time += Time.deltaTime;
        
        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);

        if (distancePlayer < meele_radius * 2) life_text.gameObject.SetActive(true);


        if (distancePlayer > awareness_radius)
        {
            //Random walk
            anim.SetBool("walking", true);
            life_text.gameObject.SetActive(false);

            /// https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
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
            agent.isStopped = false;
            anim.SetBool("walking", true);
            life_text.gameObject.SetActive(false);
            //walk torwards player
            agent.SetDestination(player.transform.position);
        }
        else if (distancePlayer < meele_radius)
        {
            anim.SetBool("walking", false);
            InstantlyTurnAttack();
            if (elapsed_time > attack_cooldown)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, meele_radius))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Player hitted");
                        hit_sound.Play();
                        anim.Play("Attacking");
                        play_contr.life -= meele_power;
                        elapsed_time = 0f;
                    }
                }
            }
        }

        InstantlyTurn(agent.destination);

        //targetDirection = agent.destination - transform.position;
        //Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0f);

        //Debug.Log(sound_cooldown.ToString());
        if (elapsed_time > sound_cooldown && distancePlayer > meele_radius)
        {
            elapsed_time = 0f;
            footsteps.volume = (1 / distancePlayer / distancePlayer);  // Inverse square law
            footsteps.Play();
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
            life_text.gameObject.SetActive(false);
            var effect = Instantiate(smoke, transform);
            Destroy(effect, .5f);
            Destroy(gameObject, .3f);
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
