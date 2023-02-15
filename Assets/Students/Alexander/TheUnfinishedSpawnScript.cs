using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheUnfinishedSpawnScript : MonoBehaviour
{
    public GameObject SpawnParticle;
    public bool Spawnin = false;
    public bool SpawnSound = false;
    public SpriteRenderer SR;
    public float Opp = 0;
    public AudioClip SpawnInSound;
    public CameraController MainCamera;
    
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
            Opp += 0.0025f;
            SpawnParticle.SetActive(true);
            //CameraController c = gameObject.GetComponent<CameraController>();
            //MainCamera.Target = SpawnParticle;
            //c.Target ==           lOOK at spawn
        }

        if (SpawnSound == true)
        {
            PlayerController p = GameObject.FindObjectOfType<PlayerController>();
            p.PlaySound(SpawnInSound);   //Play sound
            SpawnSound = false;
        }
    }
}
