using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public bool has_sword;
    public bool has_torch;
    public GameObject sword;
    public GameObject torch;
    public int meele_power = 1;
    public float meele_range = 2;
    public float knokbackDist;
    public float attack_cooldown = 1f;
    public float elapsed_time = 0f;
    public float dig_distance_threshold = 2;
    public Vector3 dig_amount = new Vector3(0,-2,0);
    public Slider healtBar;
    public TextMeshProUGUI coins_GUI;
    public AudioSource sword_slash;


    public int life = 10;
    public int max_life = 10;

    public int coins_count = 0;

    Camera cam;

    void Start()
    {
        healtBar.maxValue = life;
        cam = GameObject.FindObjectOfType<Camera>();
        has_sword = GameManager.has_sword;
        has_torch = GameManager.has_torch;
        anim = GetComponent<Animator>();
        sword.SetActive(false);
        torch.SetActive(false);
        if (GameManager.loading)
        {
            GameManager.loading = false;
            life = GameManager.save_healt;
            coins_count = GameManager.save_coint_count;
            has_sword = GameManager.save_has_sword;
            has_torch = GameManager.save_has_torch;
            if(has_sword) GameManager.has_sword = has_sword;
            if(has_torch) GameManager.has_torch = has_torch;
        }
        else
        {
            coins_count = GameManager.player_coins;
        }

        if (has_sword) meele_power++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        elapsed_time += Time.deltaTime;

        // HealtBar update
        healtBar.value = life;
        coins_GUI.SetText(coins_count.ToString());

        if (has_sword)
        {
            GameManager.has_sword = true;
            sword.SetActive(true);
        }

        if (has_torch)
        {
            GameManager.has_torch = true;
            torch.SetActive(true);
        }


        if (Input.GetMouseButtonDown(0) && elapsed_time > attack_cooldown)
        {
            anim.SetTrigger("attack");
            if (has_sword) { 
                sword_slash.Play();
            }


            RaycastHit hit;
            
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            //Check if enemy is in meele_range AND in front (Camera forward)
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, meele_range))
            {
                // Reset 
                elapsed_time = 0f;
                
                if (hit.collider.gameObject.CompareTag("Mummy"))
                {
                    Debug.Log("Enemy hitted");
                    hit.collider.gameObject.GetComponent<Mummy_AI>().life -= meele_power;
                }
            }

            if (Physics.Raycast(transform.position, transform.forward, out hit, meele_range))
            {
                if (hit.collider.gameObject.CompareTag("Cat"))
                {
                    Debug.Log("Enemy hitted");
                    hit.collider.gameObject.GetComponent<Cat_AI>().life -= meele_power;

                }
            }

            //Check if enemy is in meele_range AND in front (Camera forward)
            if (Physics.Raycast(transform.position, transform.forward, out hit, meele_range))
            {
                if (hit.collider.gameObject.CompareTag("Snake"))
                {
                    Debug.Log("Enemy hitted");
                    hit.collider.gameObject.GetComponent<SnakeController>().life -= meele_power;
                }
            }

            //Check if enemy is in meele_range AND in front (Camera forward)
            if (Physics.Raycast(transform.position, transform.forward, out hit, meele_range))
            {
                if (hit.collider.gameObject.CompareTag("Anubis"))
                {
                    Debug.Log("Enemy hitted");
                    hit.collider.gameObject.GetComponent<AnubisController>().life -= meele_power;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            //Check if enemy is in meele_range AND in front (Camera forward)
            if ( Physics.Raycast(transform.position, cam.transform.forward, out hit, Mathf.Infinity) )
            {
                if (hit.collider.gameObject.CompareTag("Sand"))
                {
                // For each vertex in a radius around the raycast hit, lower the Y value to simulate digging
                // https://docs.unity3d.com/ScriptReference/RaycastHit-triangleIndex.html

                    Debug.Log("Digging");
                    MeshCollider meshCollider = hit.collider as MeshCollider;
                    if (meshCollider == null || meshCollider.sharedMesh == null)
                        return;
                    

                    MeshFilter meshF = hit.collider.GetComponent<MeshFilter>();
                    Vector3[] vertices = meshF.sharedMesh.vertices;
                    int[] triangles = meshF.sharedMesh.triangles;
                    Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
                    Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
                    Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];

                    Transform hitTransform = hit.collider.transform;

                    p0 = hitTransform.TransformPoint(p0);
                    p1 = hitTransform.TransformPoint(p1);
                    p2 = hitTransform.TransformPoint(p2);

                    Debug.DrawLine(p0, p1, Color.red);
                    Debug.DrawLine(p1, p2, Color.red);
                    Debug.DrawLine(p2, p0, Color.red);

                    vertices[triangles[hit.triangleIndex * 3 + 0]] -= dig_amount;
                    vertices[triangles[hit.triangleIndex * 3 + 2]] -= dig_amount;
                    vertices[triangles[hit.triangleIndex * 3 + 2]] -= dig_amount;

                    // Idea to return a new modified mesh
                    // https://docs.unity3d.com/ScriptReference/Mesh-vertices.html
                    meshF.sharedMesh.vertices = vertices;
                    meshF.sharedMesh.RecalculateTangents();
                    meshF.sharedMesh.RecalculateBounds();
                    meshF.sharedMesh.RecalculateNormals();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O)) life--;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) anim.SetBool("walking", true);
        else                                                                    anim.SetBool("walking", false);

        if (Input.GetKey(KeyCode.LeftShift)) anim.SetBool("running", true);
        else                                 anim.SetBool("running", false);

    }
}
