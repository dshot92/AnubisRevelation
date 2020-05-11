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
            if(agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(Random.insideUnitSphere * walk_radius + agent.transform.position);
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
