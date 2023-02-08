using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheUnfinishedSpawnScript : MonoBehaviour
{
    public GameObject SpawnParticle;
    public bool Spawnin = false;
    public SpriteRenderer SR;
    public float Opp = 0;
    
    // Start is called before the first frame update
    void Start() 
    {
        SR.color = new Color (0, 0, 0, Opp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Spawnin == true)
        {
            SR.color = new Color (0, 0, 0, Opp);
            Opp += 0.005f;
            SpawnParticle.SetActive(true);
        }
    }
}
