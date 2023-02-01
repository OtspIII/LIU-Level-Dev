using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KevinPower : GenericPower
{
    public float Timer = 0;
    public float DashTime = 0.5f;
    public float DashXSpeed = 30;
    public float DashYSpeed = 0;

    public bool Dashing = false;
    public override void Activate()
    {
        //Timer = 1;
        Dashing = true;
        Player.SetInControl(false);
        Player.RB.gravityScale = 0;
        float dir = Player.FaceLeft ? -1 : 1;
        Player.RB.velocity = new Vector2(DashXSpeed * dir,DashYSpeed);
        //Insert code that runs when you hit 'z' here
    }

    void Update()
    {
        if (Dashing == true)  
        {
            Timer += Time.deltaTime / DashTime;
            Player.RB.gravityScale = 0;
            float dir = Player.FaceLeft ? -1 : 1;
            Player.RB.velocity = new Vector2(DashXSpeed * dir,DashYSpeed);
            Player.Body.transform.rotation = Quaternion.Euler(0, 0, Timer * 360);
            if (Input.GetKey(KeyCode.X) == false || Timer >= 2);
            {
                Player.RB.gravityScale = Player.Gravity; 
                Player.Body.transform.rotation = Quaternion.Euler(0,0,0);
                Player.SetInControl(true);
            }
        }
    }
}