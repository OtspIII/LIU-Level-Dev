using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy_AI_Cesar : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject Eye;

    private bool found;
    public float drange, speed, realspeed;
    private Vector2 dir;
    void Start()
    {
        drange = 4;
    }

   
    void Update()
    {
        dir = GMScript_Cesar.gm.pc.transform.position - transform.position;
        if(DetectArea())EnemyMovement();
        
        // if(found) Debug.Log(" I found the player");
        // else Debug.Log(" I didn't find the player");
        //
        // if (Physics2D.Raycast(transform.position, dir, drange, LayerMask.NameToLayer("Player")))
        // {
        //     found = true;
        // }
        // else found = false;
    }

    // Tracks and follows the player
    void EnemyMovement()
    {
        Vector2 vel = rb.velocity;

        if (transform.position.x > GMScript_Cesar.gm.pc.transform.position.x)
        {
            realspeed = -speed;
        }
        else
        {
            realspeed = speed;
        }

        vel.x = Mathf.Lerp(vel.x, realspeed, .05f);
        rb.velocity = vel;
    }

    //Area where the enemy detects the player
    bool DetectArea()
    {
        bool val = false;
        if (Physics2D.Raycast(transform.position, dir, drange, LayerMask.NameToLayer("Player")))
        {
            val = true;
        }
        
        else val = false;
    
        return val;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, dir.normalized * drange);
    }
}
