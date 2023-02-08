using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AlexanderPower : GenericPower
{
    public AudioClip blankUse;
    

    public override void Activate()
    {
        //Insert code that runs when you hit 'x' here
        PlayerController p = gameObject.GetComponent<PlayerController>();
        
        p.PlaySound(blankUse);
    }

    void Update()
    {

    }
}