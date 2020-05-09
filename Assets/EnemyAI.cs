using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Range(0f,500f)]
    public float awareness_radius = 10f;
    Animator anim;

    public CharacterController player;
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

        //Does enemy have line of sight with player?

        // Raycast is a line. Need a sphere of perception
        RaycastHit hit;
        
        //if (Physics.SphereCast(agent.transform.position, awareness_radius,  agent.transform.forward, out hit, 1000f))
        //{
            ///Debug.DrawRay(agent.transform.position, agent.transform.forward, Color.red, 10);
            //if he's in radius, walk to him
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
        //}

        


        //if in meele range attack

    }
}
