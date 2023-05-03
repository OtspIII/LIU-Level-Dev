using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SlamBox : MonoBehaviour
{
    [SerializeField]private List<GameObject> things, npcs;
    [SerializeField] private SlamBox parentSlam;
    public float slamForce,slamHeight;
    public bool stop,slam;


    

    private void OnTriggerEnter(Collider col)
    {
       
        if (!stop & slam)
        {
            NPCController npc = col.gameObject.GetComponent<NPCController>();
            if (npc != null || col.gameObject.layer == 10)
            {
                Debug.Log("stop is" + stop + " and I'm touching " + col.gameObject.name);
                StopCoroutine(KnockDown());
               
                HitCheck();
                stop = true;

            }
        }
        else if (!slam)
        {
            NPCController npc = col.gameObject.GetComponent<NPCController>();
            if (npc != null || col.gameObject.layer == 10) parentSlam.things.Add(col.gameObject);
           
        }
    }

    private void OnTriggerExit(Collider col)
    {
         if (!slam)
        {
            NPCController npc = col.gameObject.GetComponent<NPCController>();
            if (npc != null || col.gameObject.layer == 10) parentSlam.things.Remove(col.gameObject);
           
        }
    }


    void HitCheck()
    {
        GameObject[] thingies = things.ToArray();
        for (int i = 0; i < thingies.Length; i++)
        {

            if (thingies[i] != null && RayCheck(thingies[i].transform, thingies[i].name)) npcs.Add(thingies[i]);

        }

       
            StartCoroutine(KnockDown());
        
       
      
    }

    bool RayCheck(Transform pos, string name)
    {
        Vector3 dir = pos.position - God.Player.transform.position;
      if(Physics.Raycast( God.Player.transform.position, dir, out RaycastHit hit,100,  1 << 10));
        if (hit.collider != null)
        {
           
            if (hit.collider.gameObject.name == name) return true;
         
        }
        return false;
    }

    void TurnOffNpc(NPCController npc)
    {
        npc.InControl = false;
        npc.Behavior = NPCBehavior.Idle;
        npc.Aggro = false;
        npc.Attacking = null;
    }
    
    void TurnOnNpc(NPCController npc)
    {
        npc.InControl = true;
        npc.Behavior = NPCBehavior.Wander;
        npc.Aggro = true;
    }

    public IEnumerator KnockDown()
    {
      
        Debug.Log("stop");
       
        GameObject[] npc = npcs.ToArray();
       
        for (int i = 0; i < npc.Length; i++)
        {
            Debug.Log(npc[i].name);
            Vector3 dir = npc[i].transform.position - God.Player.transform.position;
            Vector3 slamVel = dir.normalized * (slamForce * Time.deltaTime);
            
            NPCController enemy = npc[i].gameObject.GetComponent<NPCController>();
            if (enemy != null)
            {
                enemy.RB.velocity = Vector3.zero;
                TurnOffNpc(enemy);
                slamVel += Vector3.up * slamHeight;
                enemy.RB.velocity = slamVel;
            }

            else
            {
                
            }
        }

        stop = true;
        yield return new WaitForSeconds(.1f);

        StartCoroutine(WakeEnemy(npc));

       
        npcs = new List<GameObject>();
    }

    IEnumerator WakeEnemy( GameObject[] npc)
    {
        yield return new WaitForSeconds(4f);
        
        for (int i = 0; i < npc.Length; i++)
        {
            if (npc[i] != null)
            {
                NPCController enemy = npc[i].gameObject.GetComponent<NPCController>();
                if (enemy != null)
                {
                    enemy.RB.velocity = Vector3.zero;
                    TurnOnNpc(enemy);

                }
            }
        }

       
        npcs = new List<GameObject>();
    }

    public IEnumerator ResetSlam()
    {
        yield return new WaitForSeconds(.2f);
        stop = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
