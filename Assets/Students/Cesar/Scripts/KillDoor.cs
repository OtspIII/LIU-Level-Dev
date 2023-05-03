using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillDoor : MonoBehaviour
{
    public GameObject Body;
    private Vector3 StartPos;
    public Vector3 Movement = new Vector3(0, 10, 0);
    private Vector3 DesiredPos;
    public bool AutoClose = true;
    private bool Open = false;
    public float Speed = 2;
    public List<GameObject> enemyCount;
    public TextMeshPro killFeed;

    void Start()
    {
        StartPos = Body.transform.position;
        DesiredPos = StartPos;
    }

    private void Update()
    {
        if (DesiredPos != Body.transform.position)
        {
            Body.transform.position = Vector3.Lerp(Body.transform.position, DesiredPos, Time.deltaTime * Speed);
            Body.transform.position = Vector3.MoveTowards(Body.transform.position, DesiredPos, 0.01f);
        }
        CheckTrigger();
    }

    private void CheckTrigger()
    {
        killFeed.text = "" + enemyCount.Count;
        for (int i = 0; i < enemyCount.Count; i++)
        {
            if (enemyCount[i] == null) enemyCount.Remove(enemyCount[i]);
        }
    }

    public void Trigger()
    {
        if (enemyCount.Count== 0)
        {
            if (!AutoClose && Open)
            {
                DesiredPos = StartPos;
                Open = false;
                return;
            }

            DesiredPos = StartPos + Movement;
            Open = true;
        }
    }

    public void Untrigger()
    {
        if(AutoClose)
            DesiredPos = StartPos;
    }
}
