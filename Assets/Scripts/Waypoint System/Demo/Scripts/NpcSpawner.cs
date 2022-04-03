using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{   
    private List<GameObject> spawnedNpcs = new List<GameObject>();
    public GameObject[] Npc; 
    public int SpawnCount = 1;
    public float NpcSpeed = 1f;
    void Awake()
    {
        Spawn(SpawnCount);
    }

    public void Spawn() => Spawn(1);
    public void Spawn(int spawnCount) => Spawn(spawnCount, -1);
    public void Spawn(int spawnCount, int index){
        for(int i = 0; i < spawnCount; i++){
            
            GameObject spawned = Instantiate(Npc[(index > 0)? index: Random.Range(0, Npc.Length)]);
            spawned.GetComponent<Npc>().CurrentWaypoint = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Waypoint>();
            spawned.GetComponent<Npc>().Speed = NpcSpeed;
            spawned.GetComponent<Npc>().Spawner = this;
            spawnedNpcs.Add(spawned);
        }
    }

    public void Remove(GameObject gameObject){
        spawnedNpcs.Remove(gameObject);
    }

    public GameObject GetRandomNpc => spawnedNpcs[Random.Range(0, spawnedNpcs.Count)];
}
