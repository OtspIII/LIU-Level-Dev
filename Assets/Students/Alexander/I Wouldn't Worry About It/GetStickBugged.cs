using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStickBugged : MonoBehaviour
{
    public GameObject ScreenBug;
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            ScreenBug.active = true;
        }
    }
}
