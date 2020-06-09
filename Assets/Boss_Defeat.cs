using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Defeat : MonoBehaviour
{

    AnubisController boss;

    void Start()
    {
        boss = GameObject.FindObjectOfType<AnubisController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.life <= 0)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
