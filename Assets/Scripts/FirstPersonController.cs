using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

//Battle Royale Storm
//Animating Enemies
//A Ladder
//Slow Fall
//Conveyor Belts

public class FirstPersonController : ActorController
{
    public GameObject Eyes;
    public TextMeshPro NameText;
    public float MouseSensitivity = 3;
    public bool GhostMode;
    public Vector3 StartSpot;

    public float Dizzy = 0;
    public Vector2 DizzyDir;

    public override void OnStart()
    {
        AimObj = Eyes.gameObject;
        base.OnStart();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        God.Player = this;
        God.Players.Add(this);
        StartSpot = transform.position;
        //Debug.Log("X");
        if(God.LM.UseJSON) ImprintJSON(God.LM.GetActor("Player"));
        Reset();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (God.HPText != null)
        {
            God.HPText.text = HP + "/" + GetMaxHP();
            if(GetWeapon() != null)
                God.StatusText.text = GetWeapon().Text + (Ammo > 0 ? " - " + Ammo : "");
        }

        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        
        Vector3 move = Vector3.zero;
        
        if (GhostMode)
        {
            transform.Rotate(0,xRot,0);
            Vector3 eRot = Eyes.transform.localRotation.eulerAngles;
            eRot.x += yRot;
            if (eRot.x < -180) eRot.x += 360;
            if (eRot.x > 180) eRot.x -= 360;
            eRot = new Vector3(Mathf.Clamp(eRot.x, -90, 90),0,0);
            Eyes.transform.localRotation = Quaternion.Euler(eRot);
            if (Input.GetKey(KeyCode.W))
                move += Eyes.transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= Eyes.transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= Eyes.transform.right;
            if (Input.GetKey(KeyCode.D))
                move += Eyes.transform.right;
            if (Input.GetKey(KeyCode.Space))
                move += Eyes.transform.up;
            if (Input.GetKey(KeyCode.LeftControl))
                move -= Eyes.transform.up;
            transform.position += move.normalized * GetMoveSpeed() * Time.deltaTime;
            
            return;
        }
        
        bool jump = false;
        bool sprint = false;

        if (GetMoveSpeed() > 0)
        {
            
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            if (Input.GetKey(KeyCode.LeftShift))
                sprint = true;
            if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space))
                jump = true;
        }
        HandleMove(move,jump,xRot,yRot,sprint);
        if (Input.GetMouseButton(0))
        {
            Shoot(Eyes.transform.position + Eyes.transform.forward, Eyes.transform.rotation);
        }
    }
    
    public void Reset()
    {
        Dizzy = 0;
        HP = GetMaxHP();
        RB.velocity = Vector3.zero;
        Fling = Vector3.zero;
        SetGhostMode(false);
        transform.position = StartSpot;
    }

    public override void HandleMove(Vector3 move, bool jump, float xRot, float yRot, bool sprint)
    {
        if (Dizzy > 0)
        {
            Dizzy -= Time.deltaTime;
            move.x *= -1;
            move.z *= -1;
            xRot *= -1;
            yRot *= -1;
            if(DizzyDir == Vector2.zero)
                DizzyDir = new Vector2(Random.Range(-1, 1f),Random.Range(-1, 1f));
            
            DizzyDir += new Vector2(Random.Range(-0.1f, 0.1f) + Mathf.Sin(Time.time * 3),
                Random.Range(-0.1f, 0.1f) + Mathf.Sin(Time.time * 2));
            DizzyDir.Normalize();
            xRot += DizzyDir.x * 0.5f;
            yRot += DizzyDir.y * 0.5f;
        }
        base.HandleMove(move, jump, xRot, yRot, sprint);
    }


    public void SetGhostMode(bool set)
    {
        if (set)
        {
            RB.velocity = Vector3.zero;
            MR.enabled = false;
            Coll.enabled = false;
            RB.isKinematic = true;
            GhostMode = true;
            transform.position = new Vector3(0,20,0);
        }
        else
        {
            MR.enabled = true;
            Coll.enabled = true;
            RB.isKinematic = false;
            GhostMode = false;
        }
    }

    public override void Die(ActorController source = null)
    {
        //base.Die(source);
        if(source != null)
            Debug.Log("KILLED BY " + source);
        Reset();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SetGhostMode(true);
        // if(God.LM != null && (source is FirstPersonController))
        //     God.LM.NoticeDeath(this,(FirstPersonController)source);
    }

    public IEnumerator LoadLevel(int n)
    {
        // if (God.Camera != null && God.Camera.Fader)
        // {
        //     Camera.Fader.gameObject.SetActive(true);
        //     float timer = 0;
        //     while (timer < 1)
        //     {
        //         timer = Mathf.Lerp(timer, 1.05f, 0.1f);
        //         Camera.Fader.color = new Color(0, 0, 0, timer);
        //         yield return null;
        //     }
        // }
        yield return null;

        SceneManager.LoadScene(n);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Checkpoint"))
        {
            StartSpot = transform.position;
            Debug.Log("HIT CHECKPOINT: " + other.gameObject.name);
        }
            
    }
}


