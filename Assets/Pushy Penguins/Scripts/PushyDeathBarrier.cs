using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushyDeathBarrier : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
        if (collider.tag == "Player")
        {
            //Check if players are still alive to declare a victor and score things
        }
    }
}
