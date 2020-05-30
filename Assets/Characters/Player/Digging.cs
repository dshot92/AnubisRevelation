using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{

    Renderer renderer;
    public Camera cam;
    public Vector3 displacement;
    MeshFilter filter;
    RaycastHit hit;
    public float _distance;
    public float m_Radius = 1.5f;

    void Start()
    {
        renderer = GameObject.FindObjectOfType<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, cam.transform.forward, out hit, _distance))
            {
                MeshFilter filter = hit.collider.gameObject.GetComponent<MeshFilter>();
                Mesh mesh = filter.mesh;
                Vector3[] vertices = mesh.vertices;

                for (int i = 0; i < vertices.Length; ++i)
                {
                    Vector3 v = filter.transform.TransformPoint(vertices[i]);
                    vertices[i] = vertices[i] + displacement * Time.deltaTime * Gaussian(v, hit.point, m_Radius);
                }

                mesh.vertices = vertices;
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                mesh.RecalculateTangents();


                var col = filter.GetComponent<MeshCollider>();
                if (col != null)
                {
                    var colliMesh = new Mesh();
                    colliMesh.vertices = mesh.vertices;
                    colliMesh.triangles = mesh.triangles;
                    col.sharedMesh = colliMesh;
                }
            }
        }
    }
    static float Gaussian(Vector3 pos, Vector3 mean, float dev)
    {
        //https://en.wikipedia.org/wiki/Gaussian_function
        float x = pos.x - mean.x;
        float y = pos.y - mean.y;
        float z = pos.z - mean.z;
        float n = 1.0f / (2.0f * Mathf.PI * dev * dev);
        return n * Mathf.Pow(2.718281828f, -(x * x + y * y + z * z) / (2.0f * dev * dev));
    }
}
