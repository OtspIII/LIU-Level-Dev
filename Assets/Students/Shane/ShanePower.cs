using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShanePower : GenericPower
{
    public ParticleSystem ps;
    public bool partPlay = false;
    public bool holdingJump = false;
    public float timer = 1;
    public bool charged = false;

    public bool active = false;
    
    public override void Activate()
    {
        active = true;
    }

    void Update()
    {
        float playerScaleY = Player.transform.localScale.y;
        if (active)
        {
            Debug.Log("a");
            if (Input.GetKey(KeyCode.X) && Player.OnGround())
            {
                holdingJump = true;
                if (playerScaleY > .5)
                {
                    playerScaleY -= .05f;
                }
                
                Player.SetInControl(false);
                Player.RB.velocity = Vector2.zero;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                holdingJump = false;
                playerScaleY = 1;
                if (charged)
                {
                    StartCoroutine(superJump());
                }
                else
                {
                    Player.SetInControl(true);
                }

                charged = false;
                partPlay = false;
                timer = 1;
                active = false;
            }
            Player.transform.localScale = new Vector3(1, playerScaleY, 1);
        }
        Debug.Log(active);
        Debug.Log(timer);
        if (holdingJump && timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            if (!partPlay)
            {
                ps.Play();
                partPlay = true;
            }
            charged = true;
        }
    }

    private IEnumerator superJump()
    {
        Player.RB.AddForce(new Vector2(0, 400));
        yield return new WaitForSeconds(1);
        Player.SetInControl(true);
        yield return new WaitForEndOfFrame();
    }
}