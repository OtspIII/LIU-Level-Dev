using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPower : GenericPower
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool turnOff = true;
    public override void Activate()
    {
        //Insert code that runs when you hit 'z' here
        rb = this.GetComponent<Rigidbody2D>();
        //sr = 

        if (turnOff) 
        {
            rb.gravityScale = 1;
            //sr.flipX = false;

            turnOff = false;
        }
        else 
        { 
            rb.gravityScale = 0;
            //sr.flipX = true;

            turnOff = true;
        }
    }

    void Update()
    {

    }
}