using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [HideInInspector] public Vector3 dir;
    private Rigidbody rb;
    [HideInInspector] public int damageAmount = 3;
    void Awake()
    {
        StartCoroutine(LifeTimer());
        rb = GetComponent<Rigidbody>();
        rb.velocity += (transform.forward * bulletSpeed);
    }

    
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision col)
    {
        FirstPersonController fps = col.gameObject.GetComponent<FirstPersonController>();
        if (fps != null)
        {
            fps.HP--;
            Destroy(gameObject);
        }
        else Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController fps = other.gameObject.GetComponent<FirstPersonController>();
        if (fps != null)
        {
            fps.TakeDamage( damageAmount);
            Destroy(gameObject);
        }
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
