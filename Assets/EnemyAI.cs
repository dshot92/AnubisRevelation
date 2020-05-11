using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    [Range(0f,500f)]
    public float awareness_radius = 10f;
    [Range(0f, 500f)]
    public float walk_radius = 100f;

    public TextMeshProUGUI life_text;

    Animator anim;

    public int life = 2;

    public GameObject player;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //calculate distance e direction to player.
        float distancePlayer = Vector3.Distance(agent.transform.position, player.transform.position);

        if(distancePlayer < awareness_radius)
        {
            anim.SetBool("enemy_spotted", true);
            agent.SetDestination(player.transform.position);
        }
        else
        {
            anim.SetBool("enemy_spotted", true);
            //Random walk
            /// https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
             
            // If 1/5 of destination left rework another random one
            ///TODO
            // life value could act as a swiftness multiplier, creating a more chaotically pattern based on remaining lifes points
            if(agent.remainingDistance < walk_radius / 5 )
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

        life_text.text = (life.ToString());

        //if in meele range attack

        if (life == 0)
        {
            Destroy(gameObject);
        }

    }
}
