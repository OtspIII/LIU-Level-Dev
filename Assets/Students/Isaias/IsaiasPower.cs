using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaiasPower : GenericPower
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
        Player.RB.velocity = new Vector2(DashXSpeed * dir, DashYSpeed);
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
}