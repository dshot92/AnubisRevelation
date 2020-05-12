using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public bool got_sword;
    public GameObject sword;
    public int meele_power = 1;
    public float meele_range = 2;
    public float knokbackDist;
    public float attack_cooldown = 1f;
    public float elapsed_time = 0f;
    Camera cam;

    void Start()
    {
        got_sword = false;
        anim = GetComponent<Animator>();
        sword.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsed_time += Time.deltaTime; 
        
        
        if (Input.GetMouseButtonDown(0) && elapsed_time > attack_cooldown)
        {
            anim.SetTrigger("attack");

            elapsed_time = 0f;

            RaycastHit hit;
            
            //Check if enemy is in meele_range AND in front (Camera forward)
            if(Physics.Raycast(transform.position, transform.forward, out hit, meele_range))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hitted");
                    GameObject obj = hit.collider.gameObject;
                    obj.GetComponent<EnemyAI>().life -= meele_power;
                    
                    ///KnockBack stuff
                    //Rigidbody agent = obj.GetComponent<Rigidbody>();
                    //agent.AddForce(obj.transform.forward * -1 * knokbackDist, ForceMode.Impulse);
                    
                }
            }
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
}
