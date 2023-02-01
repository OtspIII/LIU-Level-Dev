using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPower : GenericPower
{
    private PlayerController pc;
    private bool turnOff = true;
    public override void Activate()
    {
        //Insert code that runs when you hit 'x' here
        pc = this.GetComponent<PlayerController>();

        if (turnOff) 
        {
            pc.Gravity = 5;
            pc.SetYFlip(false);

            turnOff = false;
        }
        else 
        { 
            pc.Gravity = 0;
            pc.SetYFlip(true);

            turnOff = true;
        }
    }

    void Update()
    {

    }
}