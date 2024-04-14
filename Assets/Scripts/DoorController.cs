using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class DoorController : MonoBehaviour
{
    [Header("Customizable")]
    public Vector3 Movement = new Vector3(0, 10, 0);
    public float Speed = 2;
    public bool AutoClose = true;
    
    [Header("Ignore Below")]
    public GameObject Body;
    private Vector3 DesiredPos;
    private bool Open = false;
    private Vector3 StartPos;

    void Start()
    {
        StartPos = Body.transform.position;
        DesiredPos = StartPos;
    }

    private void Update()
    {
        if (DesiredPos != Body.transform.position)
        {
            Body.transform.position = Vector3.Lerp(Body.transform.position, DesiredPos, Time.deltaTime * Speed);
            Body.transform.position = Vector3.MoveTowards(Body.transform.position, DesiredPos, 0.01f);
        }
    }

    public void Trigger()
    {
        if (!AutoClose && Open)
        {
            DesiredPos = StartPos;
            Open = false;
            return;
        }
        DesiredPos = StartPos + Movement;
        Open = true;
    }

    public void Untrigger()
    {
        if(AutoClose)
            DesiredPos = StartPos;
    }
}
