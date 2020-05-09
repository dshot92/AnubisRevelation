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
    Camera cam;

    void Start()
    {
        got_sword = false;
        anim = GetComponent<Animator>();
        sword.SetActive(false);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");

            RaycastHit hit;
            if(Physics.Raycast(transform.position, cam.transform.forward, out hit, meele_range))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hitted");
                    GameObject obj = hit.collider.gameObject;
                    Rigidbody agent = obj.GetComponent<Rigidbody>();
                    agent.AddForce(obj.transform.forward * -1 * knokbackDist, ForceMode.Impulse);
                    obj.GetComponent<EnemyAI>().life -= meele_power;
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
