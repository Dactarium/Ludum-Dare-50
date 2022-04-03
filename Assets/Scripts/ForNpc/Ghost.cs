using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{   
    void Start(){
        StartCoroutine(Destroy());
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    IEnumerator Destroy(){
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
