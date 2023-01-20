using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoorCode_Alex : MonoBehaviour
{

    public AudioClip Destroyed;

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
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.PlaySound(Destroyed);
            Destroy(this.gameObject);
        }
    }
}