using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShanePower : GenericPower
{
    public ParticleSystem ps;
    public bool holdingJump = false;
    public float timer = 2;
    public bool charged = false;
    public override void Activate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            holdingJump = true;
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            holdingJump = false;
        }
    }

    void Update()
    {
        
    }
    
    
}