using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
   private void OnCollisionEnter(Collision col)
   {
      ProjectileController pc = col.gameObject.GetComponent<ProjectileController>();
      if(col.gameObject.layer == 6 || pc != null) Destroy(col.gameObject);
   }

   private void OnTriggerEnter(Collider col)
   {
      ProjectileController pc = col.gameObject.GetComponent<ProjectileController>();
      if(col.gameObject.layer == 6 || pc != null) Destroy(col.gameObject);
   }
}
