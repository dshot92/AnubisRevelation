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
    public float dig_distance_threshold = 2;
    public Vector3 dig_amount = new Vector3(0,-2,0);
    

    public int life = 10;
    public int max_life = 10;

    public Camera cam;

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
                    
                    // WIth SImple Mesh
                    
                    /*
                    Mesh mesh = meshCollider.sharedMesh;
                    
                    Vector3[] vertices = mesh.vertices;
                    int[] triangles = mesh.triangles;
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

                    vertices[triangles[hit.triangleIndex * 3 + 0]] -= dig_amount * 10;
                    vertices[triangles[hit.triangleIndex * 3 + 2]] -= dig_amount * 10;
                    vertices[triangles[hit.triangleIndex * 3 + 2]] -= dig_amount * 10;

                    // Idea to return a new modified mesh
                    // https://docs.unity3d.com/ScriptReference/Mesh-vertices.html
                    mesh.vertices = vertices;
                    mesh.RecalculateBounds();
                    mesh.RecalculateNormals();
                    */

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

                    // Way out of Reach in cost of Complexitya
                    /*
                    for (int i = 0; i < mesh.vertexCount; ++i)
                    {
                        if( Vector3.Distance(hit.transform.position, mesh.vertices[i]) < dig_distance_threshold)
                        {
                            mesh.vertices[i] -= dig_amount;
                        }
                    }
                    */
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
