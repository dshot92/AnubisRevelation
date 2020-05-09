using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Range(0f,500f)]
    public float awareness_radius = 10f;
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
            anim.SetBool("enemy_spotted", false);
            agent.SetDestination(agent.transform.position);
        }

        //if in meele range attack

        if (life == 0)
        {
            Destroy(gameObject);
        }

    }
}
