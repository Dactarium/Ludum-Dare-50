using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{   
    public float Lifespan = 1.5f;
    public bool NoEffect = false;
    [SerializeField] private GameObject _spawnEffect;
    void Start(){
        StartCoroutine(Destroy());
        if(NoEffect) return;
        _spawnEffect = Instantiate(_spawnEffect);
        _spawnEffect.transform.position = transform.position; 
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    IEnumerator Destroy(){
        yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
        if(!NoEffect) Destroy(_spawnEffect);
    }
}
