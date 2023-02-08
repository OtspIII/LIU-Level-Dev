using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Script_Cesar : MonoBehaviour
{
    [SerializeField] private GameObject holder;
    [SerializeField] private Vector3 pos;
    [SerializeField] private PlayerController pc;
    [SerializeField]  private int num,force;
    [SerializeField] private float multi;
    public TextMeshPro math;
    private float starting, multisave;
    public float adder,size;
    public Rigidbody2D rb;
    public Vector3 scale;
    private Vector3 save;
    private Vector3[] newpos;
    private Vector2[] rbforce;
    private int[] forceX = {0, 0, 0, 0} , forceY = {0,0, 0, 0};
    public Vector3 offset;
        void Start()
        {
            rbforce = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
                new Vector2(0, 0),
            };
            
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
        ShowPower();
        if(Input.GetMouseButton(0) & multi <= 2002) Charge();
       if(Input.GetKeyDown(KeyCode.E)) Activate();
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
        
        multisave += adder * Time.deltaTime;
        holder.transform.localScale += new Vector3(size * Time.deltaTime, size * Time.deltaTime, 0);
        multi = multisave;
    }
    void Activate()
    {
        pc.RB.AddRelativeForce(new Vector2(forceX[num] * multi, forceY[num] * multi), ForceMode2D.Force);
        multisave = starting;
        multi = 0;
        holder.transform.localScale = save;
    }

    void ShowPower()
    {
        
        int power = Mathf.RoundToInt((multi / 2002) * 100);
        
        math.text = power + "%";
        math.transform.position = pc.transform.position + offset;
    }
}
