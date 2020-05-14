using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEditor;

public class EnemyAI : MonoBehaviour
{
    [Range(0f,500f)]
    public float awareness_radius = 10f;
    public float meele_radius = 2f;
    [Range(0f, 500f)]
    public float walk_radius = 100f;

    bool attack = false;
    bool walk_to_player = false;
    bool random_walk= false;

    public TextMeshProUGUI life_text;

    Animator anim;

    public int life = 2;

    GameObject player;
    NavMeshAgent agent;
    AudioSource audioSource;

    float sound_cooldown = 1f;
    float elapsed_time = 0f;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Overlap step sound for 1/3 of the duration time
        sound_cooldown = audioSource.clip.length * (agent.speed/10);
    }

    void Update()
    {
        //Debug.Log(agent.destination);

        // 1) Can i hit player?
        // 2) walk to player
        // 3) if no player in sight, random walk

        //sound timer
        elapsed_time += Time.deltaTime;
        
        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);

        //Debug.Log(distancePlayer.ToString());

        if (distancePlayer < meele_radius)
        {
            attack = true;
            life_text.gameObject.SetActive(true);
        }
        else if (distancePlayer < awareness_radius)
        {
            attack = false;
            life_text.gameObject.SetActive(false);
            life_text.gameObject.SetActive(false);
            //walk torwards player
            anim.SetBool("walking", true);
            agent.SetDestination(player.transform.position);
        }
        else if (distancePlayer > awareness_radius)
        {
            attack = false;
            //Random walk
            anim.SetBool("walking", true);
            life_text.gameObject.SetActive(false);


            /// https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html

            // If 1/5 of destination left rework another random one
            ///TODO
            // life value could act as a swiftness multiplier, creating a more chaotically pattern based on remaining lifes points
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

        Debug.Log(sound_cooldown.ToString());
        if (elapsed_time > sound_cooldown && !attack)
        {
            elapsed_time = 0f;
            audioSource.volume = (1 / distancePlayer / distancePlayer);  // Inverse square law
            audioSource.Play();
        }

        // uPDATE lIFE TEXT
        life_text.text = (life.ToString());

        if (life == 0)
        {
            Destroy(gameObject);
        }

        attack = false;
        walk_to_player = false;
        random_walk = false;
    }
}
