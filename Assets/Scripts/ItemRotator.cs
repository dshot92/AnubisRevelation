using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemRotator : MonoBehaviour
{
    public float r_speed = 2f;

    // Creato just one script to handle multiples (for this case only 3, that's why i'm doing it, objects type and characteristics

    //Assign type of item from inspector and everything is handles in the trigger collision
    public bool is_heart = false;
    public bool is_amulet = false;
    public bool is_sword = false;
    public bool is_flash = false;
    public bool is_heartthird = false;

    public int healt_increase = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(Vector3.up, r_speed * Time.deltaTime);
        //transform.Rotate(Vector3.up, r_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();
            if (is_heart)
            {
                player.life += healt_increase;
                player.life %= player.max_life;
            }

            if (is_sword)
            {
                player.sword.SetActive(true);
            }

            if (is_flash)
            {
               
            }

            if (is_amulet)
            {
                GameManager.NextScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (is_heart)
            {
                player.sword.SetActive(true);
            }
            Destroy(gameObject);
        }

    }
}
