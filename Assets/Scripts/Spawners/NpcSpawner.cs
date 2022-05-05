using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{   
    
    [SerializeField] private GameObject[] _npc; 
    [SerializeField] private int _spawnCount = 1;
    [SerializeField] private float _npcSpeed = 1f;

    private Transform _waypointRoot;

    private List<GameObject> spawnedNpcs;

    void Start()
    {   
        _waypointRoot = ConfigManager.Instance.WalkableWaypointRoot.transform;

        spawnedNpcs = new List<GameObject>();
        Spawn(_spawnCount);
    }

    public void Spawn() => Spawn(1);
    public void Spawn(int spawnCount) => Spawn(spawnCount, -1);
    public void Spawn(int spawnCount, int index){
        for(int i = 0; i < spawnCount; i++){
            
            GameObject spawned = Instantiate(_npc[(index > 0)? index: Random.Range(0, _npc.Length)]);
            Npc npc = spawned.GetComponent<Npc>();
            npc.CurrentWaypoint = _waypointRoot.GetChild(Random.Range(0, _waypointRoot.childCount)).GetComponent<Waypoint>();
            npc.Speed = _npcSpeed;
            npc.Spawner = this;
            npc.name = NamePicker.Instance.RandomName(npc.nameType);

            spawned.transform.parent = transform;


            spawnedNpcs.Add(spawned);
        }
    }

    public void Spawn(Waypoint waypoint){
        GameObject spawned = Instantiate(_npc[Random.Range(0, _npc.Length)]);
        Npc npc = spawned.GetComponent<Npc>();
        npc.CurrentWaypoint = waypoint;
        npc.Speed = _npcSpeed;
        npc.Spawner = this;
        npc.name = NamePicker.Instance.RandomName(npc.nameType);

        spawned.transform.parent = transform;

        spawnedNpcs.Add(spawned);
    }

    public void Remove(GameObject gameObject){
        spawnedNpcs.Remove(gameObject);
    }

    public GameObject GetRandomNpc => spawnedNpcs[Random.Range(0, spawnedNpcs.Count)];
}
