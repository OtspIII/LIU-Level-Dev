using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Cesar : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Vector3 pos;
    [SerializeField] private PlayerController pc;
    [SerializeField]  private int num,force;
    [SerializeField] private float multi;
    private float starting;
    public float adder,size;
    public Rigidbody2D rb;
    public Vector3 scale;
    private Vector3 save;
    private Vector3[] newpos;
    private int[] forceX = {0, 0, 0, 0} , forceY = {0,0, 0, 0};
        void Start()
        {
            newpos = new Vector3[4]
            {
                new Vector3(1.7f, 0, 0),
                new Vector3(-1.7f, 0, 0),
                new Vector3(0, 1.7f, 0),
                new Vector3(0, -1.7f, 0)
            };
            forceX[0] = -force;
            forceX[1] = force;

            forceY[2] = -force;
            forceY[3] = force;
            starting = multi;
            save = holder.transform.localScale;
        }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButton(0) & multi <= 2002) Charge();
       if(Input.GetMouseButtonUp(0)) Activate();
        Change();
        pos = newpos[num];
        holder.transform.localPosition = pc.transform.position + pos;
    }

    void Change()
    {
        if (num > newpos.Length - 1) num = 0;
        if (Input.GetMouseButtonDown(1)) num++;
    }

    void Charge()
    {
        
        multi += adder * Time.deltaTime;
        holder.transform.localScale += new Vector3(size * Time.deltaTime, size * Time.deltaTime, 0);
    }
    void Activate()
    {
        pc.RB.AddRelativeForce(new Vector2(forceX[num] * multi, forceY[num] * multi), ForceMode2D.Force);
        multi = starting;
        holder.transform.localScale = save;
    }
}
