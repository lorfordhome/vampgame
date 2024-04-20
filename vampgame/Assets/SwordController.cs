using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private FirePoint fp;
    public bool canRotate;
    [Header("Weapon Stats")]
    public SwordScriptableObject swordData;
    animCallback gfx;
    // Update is called once per frame
    private void Start()
    {
        fp = FindObjectOfType<FirePoint>();
    }

    private void FindGFX()
    {
        foreach (Transform transform in this.transform)
        {
            if (transform.CompareTag("sword"))
            {
                gfx = transform.gameObject.GetComponent<animCallback>();
            }
        }
    }
    void Update()
    {
        if (gfx == null)
        {
            FindGFX();
        }
        canRotate = gfx.isAnimationDone;
        if (canRotate)
        {
            this.gameObject.transform.rotation = fp.transform.rotation;
        }
    }
}
