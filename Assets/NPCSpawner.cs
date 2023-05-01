using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject Holder;
    public List<NPCController> Children;
    public float RespawnTime = 15;
    float Countdown = 0;
    public List<NPCController> Prefabs;
    public bool SpawnEndless = false;
    bool Waves = false;

    void Start()
    {
        Waves = God.LM.Ruleset.Waves > 0;
        God.LM.NPCSpawns.Add(this);
        Spawn();
    }

    void Update()
    {
        if (Waves) return;
        if (!SpawnEndless && Children.Count > 0) return;
        Countdown -= Time.deltaTime;
        if (Countdown <= 0)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Countdown = RespawnTime;
        NPCController p = GetPrefab();
        if (p == null) return;
        NPCController n = Instantiate(GetPrefab(), Holder.transform.position, Quaternion.identity);
        n.Spawner = this;
        Children.Add(n);
    }
    
    public NPCController GetPrefab()
    {
        if (Prefabs.Count == 1) return Prefabs[0];
        if (Prefabs.Count == 0) return null;
        if (Waves)
        { 
            if(Prefabs.Count > God.LM.CurrentWave)
                return Prefabs[God.LM.CurrentWave];
        }
        return Prefabs[Random.Range(0,Prefabs.Count)];
    }

}
