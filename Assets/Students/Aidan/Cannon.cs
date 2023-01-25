using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour

{
    public Rigidbody2D playerRb;
    public float launchForce = 10f;
    public float launchAngle = 45f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LaunchPlayer();
        }
    }

    void LaunchPlayer()
    {
        Vector2 launchDirection = Quaternion.Euler(0, 0, launchAngle) * Vector2.right;
        playerRb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }
}
