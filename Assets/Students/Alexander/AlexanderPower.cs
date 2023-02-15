using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class AlexanderPower : GenericPower
{
    public AudioClip blankUse;
    public bool BlankAvailable = true;
    public float BlankCooldown = 10;
    public float BlankCooldownMax = 10;
    public GameObject BlankParticle;
    
    public override void Activate()
    {
        //Insert code that runs when you hit 'x' here
        if (BlankAvailable == true || BlankCooldown <= 0)
         {
            BlankAvailable = false;
            PlayerController p = gameObject.GetComponent<PlayerController>();
            p.PlaySound(blankUse);
            BlankCooldown = BlankCooldownMax;
            Instantiate(BlankParticle, transform.position, Quaternion.identity);
            //BlankParticle.SetActive(true);
         }
    }

    void Start()
    {
        //BlankParticle.SetActive(false);
    }

    void Update()
    {
        if (BlankAvailable == false)
        {
            BlankCooldown -= Time.deltaTime;
        }
    }
}