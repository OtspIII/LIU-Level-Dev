using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawnerPod : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> spawned;
    public float timer = 3;

    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) 
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (spawned.Count < 5 && Vector3.Distance(transform.position, player.transform.position) <= 25)
            {
                spawned.Add(Instantiate(enemies[Random.Range(0, 2)], transform.position, quaternion.identity));
                timer = Random.Range(10, 15);
            }
            
        }
    }
}
