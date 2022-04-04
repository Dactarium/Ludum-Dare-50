using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    public GameObject[] Ghost; 
    [SerializeField] private GameObject _deathLight;
    public int SpawnPerTick = 1;
    public float TickSec = .1f;
    public float Lifespan = 10f;
    void Start()
    {
        Spawn();
    }

    public void Spawn(){
        for(int i = 0; i < SpawnPerTick; i++){
            GameObject spawned = Instantiate(Ghost[Random.Range(0, Ghost.Length)]);
            spawned.transform.position = transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<Waypoint>().RandomPosition;
            spawned.transform.parent = _parent;
            spawned.transform.forward = spawned.transform.up;
            spawned.GetComponent<Ghost>().Lifespan = Lifespan;

            GameObject deathLight = Instantiate(_deathLight);
            deathLight.transform.position = new Vector3(spawned.transform.position.x, 0, spawned.transform.position.z);
            StartCoroutine(DestroyAfterLifeSpan(deathLight));
        }
        
        StartCoroutine(Tick());
    }

    IEnumerator Tick(){
        yield return new WaitForSeconds(TickSec);
        Spawn();
    }

    IEnumerator DestroyAfterLifeSpan(GameObject light){
        yield return new WaitForSeconds(Lifespan);
        Destroy(light);
    }
}
