using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{
    public Vector3 Size;

    // Start is called before the first frame update
    void Start()
    {
        Size = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && transform.lossyScale != Size)
        {
            Transform parent = transform.parent;
            transform.parent = null;
            transform.localScale = Size;
            transform.parent = parent;
        }
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

        if (other.gameObject.name == "LockedDoorTwo")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}