using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    [HideInInspector] public FirstPersonController fps;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.gameObject.GetComponent<FirstPersonController>();
        if (player != null) fps = player;
    }
}
