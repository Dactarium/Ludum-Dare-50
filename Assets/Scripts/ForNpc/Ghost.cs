using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{   
    public float Lifespan = 1.5f;

    void Start(){
        StartCoroutine(Destroy());
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    IEnumerator Destroy(){
        yield return new WaitForSeconds(Lifespan);
        Destroy(gameObject);
    }
}
