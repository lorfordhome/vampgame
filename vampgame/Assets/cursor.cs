using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimCursor : MonoBehaviour
{

    public float speed;
    public Transform target;
    FirePoint fp;
    private Vector3 zAxis = new Vector3(0, 0, 1);

    private void Start()
    {
        fp=FindAnyObjectByType<FirePoint>();
    }
    void Update()
    {
        transform.RotateAround(target.position, zAxis, speed);
        transform.rotation = fp.transform.rotation;
    }


}