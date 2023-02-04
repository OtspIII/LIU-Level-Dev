using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashGameCode_Alex : MonoBehaviour
{
    // Its all in the name :D
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            Application.Quit();
        }
    }
    
}
