﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController PC;
    public CheckpointController LastCheckpoint;
    public static int DeathCount = 0;
    public float Speed = 10;
    public float JumpPower = 10;
    public float JumpTime = 0.5f;
    public float Gravity = 2;
	float GravityStart = 2;
    public int MaxAirJumps = 0;
    public SpriteRenderer Body;
    public BoxCollider2D Foot;
    public AudioSource AS;
    
    public Rigidbody2D RB;
    public bool FaceLeft = false;
	public bool StandOnHead = false;
    private float JumpTimer = 0;
    public List<GameObject> Floors = new List<GameObject>();
    private GenericPower Power;
    private bool InControl = true;
    private int AirJumps;
    public float FallPlatTime = 0;

    public float  MaxHP = 0;
    public float HP = 0;
    
    public Vector2 KBVel = new Vector2();
    public Vector2 KBDesired = new Vector2();
    public bool JustKB = false;

    private void Awake()
    {
        PlayerController.PC = this;
        HP = MaxHP;
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.gravityScale = 0;
		GravityStart = Gravity;
        Power = GetComponent<GenericPower>();
        AS = GetComponent<AudioSource>();
    }

    void Update()
    {
        FallPlatTime -= Time.deltaTime;
        if (!InControl) return;      
        
		bool onGround = OnGround();
        Vector2 vel = KBDesired;
		if(!onGround)
        	vel.y -= Gravity * Time.deltaTime * 9.8f;
		else
			vel.y = 0;
        
        float xDesire = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            xDesire = Speed;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            xDesire = -Speed;
        if (Mathf.Sign(xDesire) != Mathf.Sign(vel.x))
            vel.x = 0;
        vel.x = Mathf.Lerp(vel.x, xDesire, 0.25f);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (onGround)
            {
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    FallPlatTime = 0.5f;
                    JumpTimer = 999;
                    vel.y = -3f;
                }
                else
                {
                    vel.y = JumpPower;
                    JumpTimer = 0;
                    AirJumps = MaxAirJumps;
                }
            }
            else if (JumpTimer < JumpTime)
            {
                JumpTimer += Time.deltaTime;
                vel.y = JumpPower;
            }
            else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z)) && AirJumps > 0)
            {
                AirJumps--;
                JumpTimer = 0;
                vel.y = JumpPower;
            }
        }
        else
            JumpTimer = 999;

        KBDesired = vel;
        RB.velocity = vel + KBVel; 
        if (xDesire != 0)
            SetFlip(vel.x < 0);
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.X) && Power != null)
            Power.Activate();
        if (Input.GetKeyDown(KeyCode.R))
            Die(gameObject);

        if (RB.velocity.y > 0 || FallPlatTime > 0)
        {
            gameObject.layer = LayerMask.NameToLayer("RisingPlayer");
            Foot.gameObject.layer = LayerMask.NameToLayer("RisingPlayerFoot");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            Foot.gameObject.layer = LayerMask.NameToLayer("Foot");
        }
            
    }

    void FixedUpdate()
    {
        JustKB = false;
    }

    public void SetFlip(bool faceLeft)
    {
        if (faceLeft == FaceLeft) return;
        FaceLeft = faceLeft;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (FaceLeft ? -1 : 1),
            transform.localScale.y,1);
    }
	public void SetYFlip(bool standOnHead)
    {
        if (standOnHead == StandOnHead) return;
        StandOnHead = standOnHead;
        Body.transform.localScale = new Vector3(Body.transform.localScale.x,Mathf.Abs(Body.transform.localScale.y) * (StandOnHead ? -1 : 1),1);
    }

    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    public void SetInControl(bool inControl)
    {
        InControl = inControl;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.otherCollider.gameObject == Foot.gameObject)
        {
            if(!JustKB)
                KBVel = Vector2.zero;
            if(!Floors.Contains(other.gameObject))
                Floors.Add(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.otherCollider.gameObject == Foot.gameObject)
            Floors.Remove(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckpointController cc = other.gameObject.GetComponent<CheckpointController>();
        if (cc != null)
        {
            SetCheckpoint(cc);
            cc.GetChecked();
        }
    }

    private IEnumerator DeathAnimation(GameObject source)
    {
        SetInControl(false);
        Vector2 toss = new Vector2(Random.Range(-10,10),Random.Range(-10,10));
        if (source != null)
        {
            Vector2 dist = source.transform.position - transform.position;
            if (Mathf.Abs(dist.x) >= Mathf.Abs(dist.y))
            {
                toss.x = 10 * (dist.x > 0 ? -1 : 1);
                toss.y = Mathf.Abs(toss.y) * (RB.velocity.y >= 0 ? 1 : -1);
            }
            else
            {
                toss.y = 10 * (dist.y > 0 ? -1 : 1);
                toss.x = Mathf.Abs(toss.x) * (RB.velocity.x >= 0 ? 1 : -1);
            }
        }
        RB.velocity = toss;
        CameraController Camera = FindObjectOfType<CameraController>();
        if (Camera != null && Camera.Fader != null)
        {
            Camera.Fader.gameObject.SetActive(true);
            float spin = 0;
            float timer = 0;
            while (timer < 1)
            {
                spin += Time.deltaTime * 1000 * (RB.velocity.x >= 0 ? -1 : 1);
                Body.transform.rotation = Quaternion.Euler(0,0,spin);
                timer = Mathf.Lerp(timer, 1.01f, 0.07f);
                Camera.Fader.color = new Color(0, 0, 0, timer);
                yield return null;
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TakeDamage(GameObject source,int amt = 1)
    {
        HP -= amt;
        if (HP <= 0)
            Die(source);
    }
    
    public void Die(GameObject source,bool force=false)
    {
		if(!force && Power != null && Power.DeathOverride(source)) return;
        DeathCount++;
        Debug.Log("YOU DIED: " + DeathCount + " / " + SceneManager.GetActiveScene().name.ToUpper());
        if (LastCheckpoint == null)
            StartCoroutine(DeathAnimation(source));
        else
            transform.position = LastCheckpoint.Spawn.position;
    }
    
    public void SetCheckpoint(CheckpointController cc){
        LastCheckpoint = cc;
    }

    public void Knockback(Vector2 dir)
    {
        JustKB = true;
        KBVel = dir;
    }

    public void PlaySound(AudioClip clip)
    {
        AS.PlayOneShot(clip);
    }

	public void SetGravity(float grav){
		Gravity = GravityStart * grav;
	}
}
