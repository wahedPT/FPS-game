using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    public float targetMoveSpeed;
    public float targetRotateSpeed;
    public bool isMove, isRotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.position += new Vector3(targetMoveSpeed, 0, 0) * Time.deltaTime;

        }
        if (isRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles+new Vector3(0, targetRotateSpeed, 0f) * Time.deltaTime);
        }
    }
}
