using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawnerPod : MonoBehaviour
{
    public List<GameObject> enemies;
    public float timer = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(enemies[Random.Range(0, 2)], transform.position, quaternion.identity);
            timer = Random.Range(10, 15);
        }
    }
}
