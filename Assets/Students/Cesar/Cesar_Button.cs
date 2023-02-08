using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cesar_Button : MonoBehaviour
{
    [SerializeField] private MovingPlatformController[] move;
    [SerializeField] private Vector3[] pos;
    public Vector3 offset;
    private bool pressed,stop;
    private float timer;
    public float settimer;
    
    private void Start()
    {
        timer = settimer;

        foreach (var i in move)
        {
            i.Destinations[0] = i.transform.position;
        }
       
    }

    void Update()
    {
       
        if (pressed)
        {
            
            foreach (var i in move)
            {

              
                GetComponent<SpriteRenderer>().color = Color.gray;
                if (!stop)
                {
                    i.Destinations[0] = i.transform.position + offset;
                    stop = true;
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    i.Destinations[0] = i.transform.position + -offset;
                    pressed = false;
                    stop = false;
                    timer = settimer;
                }
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") & !pressed) pressed = true;
    }
}
