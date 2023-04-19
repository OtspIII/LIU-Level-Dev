using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNormal : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public DetectionRange detect;
    public Transform playerPos;
    public GameObject bulletPrefab;
    public float rotationSpeed;
    public float sightDistance;
    public float attackDistance;
    void Awake()
    {
        AISetup();
        ComponentSetup();
    }

    
    void Update()
    {
      
    }

    public void AISetup()
    {
        nav = GetComponent<NavMeshAgent>();
       
    }

    public void ComponentSetup()
    {
        // playerPos = God.Players[0].transform;
    }


    public virtual void Attack()
    {
        
    }
    
    public bool AttackPlayer(Transform pos,LayerMask layer, float dist)
    {
        Vector3 rayDir = God.Players[0].transform.position - pos.position;
        
        RaycastHit hit  = new RaycastHit();
        Ray ray = new Ray(pos.position, rayDir);
        if (Physics.Raycast(ray, out hit, dist, layer))
        {
            if (hit.collider != null)
            {
              
                if (hit.collider.gameObject.layer == 8) return true;
            }
        }
        return false;
    }  
    public bool SeekPlayer(Transform pos ,LayerMask layer, float dist)
    {
        Vector3 rayDir = God.Players[0].transform.position - pos.position;
        
        RaycastHit hit  = new RaycastHit();
        Ray ray = new Ray(pos.position, rayDir);
        if (Physics.Raycast(ray, out hit, dist, layer))
        {
            
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 8) return true;
            }
        }

        return false;
    }
    public void FacePlayer(Transform pos)
    { 
        dir = God.Players[0].transform.position - pos.position;
       Quaternion lookRotation = Quaternion.LookRotation(dir.normalized);
       pos.transform.rotation = Quaternion.Slerp(pos.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
