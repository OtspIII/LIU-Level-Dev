using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickRoll : MonoBehaviour
{
    public GameObject Screen;
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            Screen.active = true;
        }
    }
}
