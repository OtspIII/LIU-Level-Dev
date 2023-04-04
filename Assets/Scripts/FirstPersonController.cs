using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class FirstPersonController : MonoBehaviour
{
    public Camera Eyes;
    public Rigidbody RB;
    public NetworkRigidbody NRB;
    public MeshRenderer MR;
    public Collider Coll;
    public TextMeshPro NameText;
    public float MouseSensitivity = 3;
    public float JumpPower = 7;
    public List<GameObject> Floors;
    public float ShotCooldown;
    public bool JustKnocked = false;
    public bool GhostMode;

    public JSONWeapon CurrentWeapon;
    public JSONWeapon DefaultWeapon;
    
    public Vector3 Position;
    public Vector3 Fling;
    public float XRot;
    public float YRot;
    public int HP;
    public int Ammo;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        God.Players.Add(this);

    }


    public void SetWeapon(JSONWeapon wpn)
    {
        CurrentWeapon = wpn;
        Ammo = wpn.Ammo;
    }


    public JSONWeapon GetWeapon()
    {
        // return God.LM.GetWeapon(Weapon.Value.ToString());
        if (CurrentWeapon != null) return CurrentWeapon;
        if (DefaultWeapon != null) return DefaultWeapon;
        return null;
    }
    
    void Update()
    {
        JustKnocked = false;
        if (transform.position.y < -100)
            Die();
        if (God.HPText != null)
        {
            God.HPText.text = HP + "/" + GetMaxHP();
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
        ShotCooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0) && ShotCooldown <= 0)
        {
            JSONWeapon wpn = GetWeapon();
            if (wpn != null && wpn.RateOfFire > 0)
            {
                ShotCooldown = wpn.RateOfFire;
                Shoot(Eyes.transform.position + Eyes.transform.forward, Eyes.transform.rotation);
            }
        }
        
        
    }

    public void HandleMove(Vector3 move,bool jump, float xRot,float yRot,bool sprint)
    {
        bool onGround = OnGround();
        move = move.normalized * (sprint ? GetSprintSpeed() : GetMoveSpeed());
        if (jump && onGround)
            move.y = JumpPower;
        else
            move.y = RB.velocity.y;
        if (Fling.x != 0)
            move.x += Fling.x;
        if (Fling.z != 0)
            move.z += Fling.z;
        if (Fling != Vector3.zero && move.y == 0) move.y = 3;
        RB.velocity = move;
        transform.Rotate(0,xRot,0);
        Vector3 eRot = Eyes.transform.localRotation.eulerAngles;
        eRot.x += yRot;
        if (eRot.x < -180) eRot.x += 360;
        if (eRot.x > 180) eRot.x -= 360;
        eRot = new Vector3(Mathf.Clamp(eRot.x, -90, 90),0,0);
        Eyes.transform.localRotation = Quaternion.Euler(eRot);
    }
    
    
    public void Shoot(Vector3 pos,Quaternion rot)
    {
        JSONWeapon wpn = GetWeapon();
        if (Ammo > 0)
        {
            Ammo--;
            if (Ammo <= 0)
                SetWeapon(DefaultWeapon);
        }
        for (int n = 0; n < Math.Max(1, wpn.Shots); n++)
        {
            Vector3 r = rot.eulerAngles;
            r.y += Random.Range(-wpn.Accuracy, wpn.Accuracy);
            r.x += Random.Range(-wpn.Accuracy, wpn.Accuracy);
            ProjectileController p = Instantiate(God.Library.Projectile, pos,Quaternion.Euler(r));
            p.Setup(this,wpn);
        }
    }
    

    public bool OnGround()
    {
        return Floors.Count > 0;// && Physics.Raycast(transform.position,transform.position + new Vector3(0,-5,0),1.5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
        if (Fling != Vector3.zero && !JustKnocked)
        {
//            Debug.Log("ENDFLING");
            Fling = Vector3.zero;
            
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Floors.Remove(other.gameObject);
    }

    public int GetMaxHP()
    {
        return God.LM != null && God.LM.Ruleset != null && God.LM.Ruleset.PlayerHP > 0 ? God.LM.Ruleset.PlayerHP : 100;
    }
    
    public float GetMoveSpeed()
    {
        return God.LM != null && God.LM.Ruleset != null && God.LM.Ruleset.MoveSpeed > 0 ? God.LM.Ruleset.MoveSpeed : 10;
    }
    
    public float GetSprintSpeed()
    {
        float move = GetMoveSpeed();
        return God.LM != null && God.LM.Ruleset != null && God.LM.Ruleset.SprintSpeed > 0 ? God.LM.Ruleset.SprintSpeed * move : move * 1.5f;
    }
    
    public void Reset()
    {
        HP = GetMaxHP();
        RB.velocity = Vector3.zero;
        Fling = Vector3.zero;
        SetGhostMode(false);
    }

    public void TakeDamage(int amt, FirstPersonController source = null)
    {
        HP -= amt;
        if (HP <= 0)
        {
            Die(source);
        }
    }
    
    
    public void TakeHeal(int amt)
    {
        HP += amt;
        if (HP > GetMaxHP())
        {
            HP = GetMaxHP();
        }
    }

    public void SetGhostMode(bool set)
    {
        Debug.Log("SGM");
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

    public void Die(FirstPersonController source=null)
    {
        Debug.Log("KILLED BY " + source);
        
        // if(God.LM.Respawn(this))
        //     Reset();
        // else
            SetGhostMode(true);
        if(God.LM != null)
            God.LM.NoticeDeath(this,source);
    }

    // public override void OnDestroy()
    // {
    //     base.OnDestroy();
    //     God.Players.Remove(this);
    //     if (God.LM != null) God.LM.RemovePlayer(this);
    // }

    public void TakeKnockback(Vector3 kb)
    {
        RB.velocity = kb;
        Fling = new Vector3(kb.x,0,kb.z);
//        Debug.Log("KB: " + kb);
        JustKnocked = true;
        RB.velocity = kb;
        JustKnocked = true;
    }
    // public void SetTeam(IColors team)
    // {
    //     if (!IsServer) return;
    //     Team = team;
    // }
}


