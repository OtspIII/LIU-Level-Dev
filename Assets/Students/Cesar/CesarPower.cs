using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CesarPower : GenericPower
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Vector3 pos;
    public override void Activate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) pos = new Vector3(0, 1.7f, 0);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) pos = new Vector3(0, -1.7f, 0);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) pos = new Vector3(1.7f, 0, 0);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) pos = new Vector3(-1.7f,0, 0);
    }

    void Update()
    {
        holder.transform.localPosition = pos;
    }
}