using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        float Xvalue = Random.Range(-20f,20f);
        float Zvalue = Random.Range(-20f,20f);
        GameObject temp = Instantiate(prefab);
        temp.transform.position =new Vector3(Xvalue, 1f, Zvalue);
    }
}
