using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngineInternal;

public class TipsAlphaDistance : MonoBehaviour
{
    float starting_distance;
    public float distance;
    GameObject player;
    TextMeshProUGUI prompt;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prompt = GetComponent<TextMeshProUGUI>();
        starting_distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        //Debug.Log("distance: " + distance + "\n" + "starting_distance: " + starting_distance + "\n" + "Alpha : " + (distance - starting_distance) * 255);

        double result = Mathf.Lerp(255, 0, Mathf.InverseLerp(0,starting_distance, distance));
        Debug.Log(new Color32(255, 0, 255, (byte)((int)(result) % 256)));
        prompt.faceColor = new Color32(255,0,255, (byte)((int)(result) % 256));
        //prompt.faceColor =  (byte)((int)(result * 255) % 256);
        prompt.UpdateFontAsset();
        prompt.UpdateVertexData();

    */
    }
}
