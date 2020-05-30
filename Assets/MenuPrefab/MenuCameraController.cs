using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Permissions;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuCameraController : MonoBehaviour
{
    public float damp = 100000000f;

    void LateUpdate()
    {
        float mouseY = Input.mousePosition.x - Screen.width / 2;
        float mouseX = Input.mousePosition.y - Screen.height / 2;
        
        //Debug.Log(mouseX.ToString() + " - " + mouseY.ToString());
        transform.Rotate(new Vector3(mouseX, -mouseY, 0) / damp * Time.deltaTime);
        Mathf.Clamp(transform.rotation.x, -1,1);
        Mathf.Clamp(transform.rotation.y, -1,1);
    }
}
