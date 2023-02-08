using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MishaPower : GenericPower
{
    public float Timer = 0;
    public float DashTime = 0.5f;
    public float DashXSpeed = 30;
    public float DashYSpeed = 0;
    
    public override void Activate()
    {
        Timer = 1;
        Player.SetInControl(false);
        Player.SetGravity(0);
        float dir = Player.FaceLeft ? -1 : 1;
        Player.RB.velocity = new Vector2(DashXSpeed * dir,DashYSpeed);
//        float dist = Vector2.Distance(transform.position, brick.transform.position);
    }

    void Update()
    {
        if (Timer > 0)  
        {
            Timer -= Time.deltaTime / DashTime;
            Player.Body.transform.rotation = Quaternion.Euler(0, 0, Timer * 360);
            if (Timer <= 0)
            {
                Player.SetGravity(1);
                Player.Body.transform.rotation = Quaternion.Euler(0,0,0);
                Player.SetInControl(true);
            }
        }
    }

//    public override bool DeathOverride(GameObject source)
//    {
//        Player.Body.color = Color.red;
//        return true;
//    }

	
}
