using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoorCode_Alex : MonoBehaviour
{
    public AudioClip Destroyed;
    public DialogueScript DScript;
    
    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.PlaySound(Destroyed);
            Destroy(this.gameObject);
            DScript.startDialogue = true;
        }
    }
}