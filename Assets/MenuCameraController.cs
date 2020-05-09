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
        float mouseX = Input.mousePosition.x - Screen.width/2;
        float mouseY = Input.mousePosition.y - Screen.width/2 ;
        Debug.Log(mouseX.ToString()+ " - " + mouseY.ToString());
        transform.Rotate(new Vector3(mouseX,mouseY, 0).normalized / damp);
    }
}
