using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybindings : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //transform.forward = player.transform.forward * -1;
        //transform.LookAt(player.transform.position, Vector3.up);
        //transform.Rotate(new Vector3(90, 180, 0));
    }
}
