using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActorController : MonoBehaviour
{
    public GameObject AimObj;
    public Rigidbody RB;
    public MeshRenderer MR;
    public Collider Coll;
    public float JumpPower = 7;
    public float MoveSpeed = 10;
    public float SprintSpeed = 1.5f;
    public List<GameObject> Floors;
    public float ShotCooldown;
    public bool JustKnocked = false;
    
    public JSONWeapon CurrentWeapon;
    public JSONWeapon DefaultWeapon;
    
    public Vector3 Fling;
    public int HP;
    public int Ammo;

    void Start()
    {
      OnStart();  
    }

    public virtual void OnStart()
    {
        God.Actors.Add(this);
    }

    void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        JustKnocked = false;
        if (transform.position.y < -100)
            Die();
        ShotCooldown -= Time.deltaTime;

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
    
    public virtual void Die(ActorController source=null)
    {
        Debug.Log("KILLED BY " + source);
        
        // if(God.LM.Respawn(this))
        //     Reset();
        // else
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
        Vector3 eRot = AimObj.transform.localRotation.eulerAngles;
        eRot.x += yRot;
        if (eRot.x < -180) eRot.x += 360;
        if (eRot.x > 180) eRot.x -= 360;
        eRot = new Vector3(Mathf.Clamp(eRot.x, -90, 90),0,0);
        AimObj.transform.localRotation = Quaternion.Euler(eRot);
    }
    
    
    public void Shoot(Vector3 pos,Quaternion rot)
    {
        JSONWeapon wpn = GetWeapon();
        if (wpn == null || wpn.RateOfFire <= 0) return;
        ShotCooldown = wpn.RateOfFire;

        if (Ammo > 0)
        {
            Ammo--;
            if (Ammo <= 0)
                SetWeapon(DefaultWeapon);
        }
        for (int n = 0; n < Mathf.Max(1, wpn.Shots); n++)
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
        return Floors.Count > 0 && Physics.Raycast(transform.position,transform.up * -1,1.5f);
    }
    
    public void TakeDamage(int amt, ActorController source = null)
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
    
    public void TakeKnockback(Vector3 kb)
    {
        RB.velocity = kb;
        Fling = new Vector3(kb.x,0,kb.z);
//        Debug.Log("KB: " + kb);
        JustKnocked = true;
        RB.velocity = kb;
        JustKnocked = true;
    }
    
    public virtual int GetMaxHP()
    {
        return 100;
    }
    
    public virtual float GetMoveSpeed()
    {
        return MoveSpeed;
        //return God.LM != null && God.LM.Ruleset != null && God.LM.Ruleset.MoveSpeed > 0 ? God.LM.Ruleset.MoveSpeed : 10;
    }
    
    public virtual float GetSprintSpeed()
    {
        float move = GetMoveSpeed();
        if (SprintSpeed > 0) move *= SprintSpeed;
        return move;
        //return God.LM != null && God.LM.Ruleset != null && God.LM.Ruleset.SprintSpeed > 0 ? God.LM.Ruleset.SprintSpeed * move : move * 1.5f;
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

    private void OnDestroy()
    {
        God.Actors.Remove(this);
    }
}
