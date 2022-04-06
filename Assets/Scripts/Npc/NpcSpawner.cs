using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{   
    private List<GameObject> spawnedNpcs;
    [SerializeField] private Transform _parent;
    public GameObject[] Npc; 
    public int SpawnCount = 1;
    public float NpcSpeed = 1f;

    void Start()
    {   
        spawnedNpcs = new List<GameObject>();
        Spawn(SpawnCount);
    }

    public void Spawn() => Spawn(1);
    public void Spawn(int spawnCount) => Spawn(spawnCount, -1);
    public void Spawn(int spawnCount, int index){
        for(int i = 0; i < spawnCount; i++){
            
            GameObject spawned = Instantiate(Npc[(index > 0)? index: Random.Range(0, Npc.Length)]);
            Npc npc = spawned.GetComponent<Npc>();
            npc.CurrentWaypoint = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Waypoint>();
            npc.Speed = NpcSpeed;
            npc.Spawner = this;
            npc.name = NamePicker.Instance.RandomName(npc.nameType);

            spawned.transform.parent = _parent;


            spawnedNpcs.Add(spawned);
        }
    }

    public void Spawn(Waypoint waypoint){
        GameObject spawned = Instantiate(Npc[Random.Range(0, Npc.Length)]);
        Npc npc = spawned.GetComponent<Npc>();
        npc.CurrentWaypoint = waypoint;
        npc.Speed = NpcSpeed;
        npc.Spawner = this;
        npc.name = NamePicker.Instance.RandomName(npc.nameType);

        spawned.transform.parent = _parent;

        spawnedNpcs.Add(spawned);
    }

    public void Remove(GameObject gameObject){
        spawnedNpcs.Remove(gameObject);
    }

    public GameObject GetRandomNpc => spawnedNpcs[Random.Range(0, spawnedNpcs.Count)];
}
