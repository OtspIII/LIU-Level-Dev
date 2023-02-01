using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            transform.parent = other.transform;
            transform.localPosition = new Vector3(0.78f, 0, 0);
        }

        if (other.gameObject.name == "LockedDoor")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
