using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private FirePoint fp;
    public bool canRotate;
    // Update is called once per frame
    private void Start()
    {
        fp = FindObjectOfType<FirePoint>();
    }
    void Update()
    {
        if (canRotate)
        {
            this.gameObject.transform.rotation = fp.transform.rotation;
        }
    }
}
