using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPower : MonoBehaviour
{
    public PlayerController Player;

    private void Awake()
    {
        Player = GetComponent<PlayerController>();
    }

    public virtual void Activate()
    {
        
    }

	public virtual bool DeathOverride(GameObject source){
		return false;
	}
}
