using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidanPower : GenericPower
{
public enum PlayerStates
{
Sprinting,
Sliding
}
public float sprintSpeed = 10f;
public float slideSpeed = 20f;
public float forceAmount = 10f;
public float slideUnderObjectThreshold = 1f;
public PlayerStates currentPlayerState;

private Rigidbody2D playerRigidbody;
private float playerHeight;
private bool isSprinting;
private bool isSliding;
public float SlidePower = 4;

/*public override void Awake()
{
  //  base.Awake();
    playerRigidbody = GetComponent<Rigidbody2D>();
    if (playerRigidbody == null)
    {
        Debug.LogError("Rigidbody2D component not found on " + name);
        return;
    }
    playerHeight = GetComponent<BoxCollider2D>().size.y;
}
*/
private void Start()
{
    playerRigidbody = GetComponent<Rigidbody2D>();
}

void Update()
{
    if (Input.GetKey(KeyCode.LeftShift))
    {
        //playerRigidbody.AddForce(new Vector2(forceAmount, 0));
        isSprinting = true;
        isSliding = true;   
    }
    else
    {
        isSprinting = false;
        isSliding = false;
        SlidePower = 1;
    }

    
}

private void FixedUpdate()
{
    //CheckForSlideUnderObject();
        //HandleSprint();
        HandleSlide();
}

void CheckForSlideUnderObject()
{
    Vector2 raycastStart = transform.position + new Vector3(0, playerHeight / 2, 0);
    RaycastHit2D hit = Physics2D.Raycast(raycastStart, Vector2.right, slideUnderObjectThreshold);
    if (hit.collider != null)
    {
        isSliding = true;
    }
}

void HandleSprint()
{
    if (isSprinting)
    {
        playerRigidbody.velocity = new Vector2(sprintSpeed, playerRigidbody.velocity.y);
        currentPlayerState = PlayerStates.Sprinting;
    }
}

void HandleSlide()
{
    if (isSliding)
    {
        float dir = 1;
        if (Player.FaceLeft == true) dir = -1;
       // Player.SetInControl(true);
       playerRigidbody.velocity = new Vector2(slideSpeed*SlidePower*dir, playerRigidbody.velocity.y);
        transform.localScale = new Vector3(1f, 0.35f, 0.35f);
        currentPlayerState = PlayerStates.Sliding;
        SlidePower = Mathf.Lerp(SlidePower, 0, 0.1f);
    }
    else
    {
       // Player.SetInControl(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
}
