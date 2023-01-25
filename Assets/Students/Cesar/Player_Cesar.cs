using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cesar : MonoBehaviour
{
    public PlayerController pc;
    public List<AI_Enemy_Cesar> enemy;
    public MovingPlatformController move;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(pc.HP <= 0) Restart(); 
    }

    void Restart()
    {
        pc.HP = 3;
        foreach (var i in enemy)
        {
            i.transform.localPosition = new Vector3(0, 0, 0);
            i.found = false;
            i.rb.velocity = new Vector2(0, 0);
        }
        move.transform.position = new Vector3(104.82f, -1.29f, 0);
        move.Speed = 0;
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 12)
        {
            MovingPlatformController mc = col.gameObject.GetComponent<MovingPlatformController>();
            if (mc != null & mc.Speed == 0) mc.Speed = .05f;
        }

       
    }
}
