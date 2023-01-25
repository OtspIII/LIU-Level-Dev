using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy_Cesar : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform Eye;
    public GameObject mark;

    public bool found, attack,ani,left;
    public float drange, speed, realspeed;
    private Vector2 dir,force;
    void Start()
    {
        drange = 12;
    }

    // Update is called once per frame
    void Update()
    {
        dir = GMScript_Cesar.gm.pc.transform.position - transform.position;
        if(found)EnemyMovement();
        if (attack & !ani) StartCoroutine(AttackAni());
        
        DetectPlayer();
        
    }
    
    void EnemyMovement()
    {
        if (transform.position.x > GMScript_Cesar.gm.pc.transform.position.x)
        {
            left = false;
            Eye.localPosition = new Vector3(-0.249f, 0.163f, 29.1f);
            force = new Vector2(-230f, 250);
        }
        else
        {
            left = true;
            Eye.localPosition = new Vector3(0.249f, 0.163f, 29.1f);
            force = new Vector2(230f, 250);
        }
        
        Vector2 vel = rb.velocity;
        if (!attack)
        {
            if (!left)
            {
                realspeed = -speed;
                
            }
            else realspeed = speed;
              

            vel.x = Mathf.Lerp(vel.x, realspeed, 1.3f * Time.deltaTime);
        }
        // else if(attack & !ani) vel.x = Mathf.Lerp(vel.x, 0, 2f * Time.deltaTime);

        rb.velocity = vel;
    }

    void DetectPlayer()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, drange);
        if (hit.collider != null)
        {
            PlayerController pc = hit.collider.GetComponent<PlayerController>();
            if (pc != null)
            {
                
                found = true;
            }
        }

        

        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, dir, drange / 2.3f);
        if (hit2.collider != null)
        {
            PlayerController pc2 = hit2.collider.GetComponent<PlayerController>();
            if (pc2 != null)
            {

                attack = true;
            }

        }
        else attack = false;
        
    }

    IEnumerator AttackAni()
    {
        ani = true;
        Debug.Log("Attacking");
        mark.SetActive(true);
        yield return new WaitForSeconds(.7f);
        if (attack)
        {
            
            mark.SetActive(false);
            rb.AddForce(force, ForceMode2D.Force);
        }
        else mark.SetActive(false);

        yield return new WaitForSeconds(1f);
        ani = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        if(pc != null) rb.AddForce(force * new Vector2(-1,1), ForceMode2D.Force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, dir.normalized * drange);
    }
}
