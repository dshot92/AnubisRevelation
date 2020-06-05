using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class LifeTextEnemy : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //transform.forward = player.transform.forward * -1;
        transform.LookAt(player.transform.position + new Vector3(0,2,0), Vector3.up);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
