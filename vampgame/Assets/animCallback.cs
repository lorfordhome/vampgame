using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animCallback : MonoBehaviour
{
    public bool isAnimationDone=true;

    void animStart()
    {
        isAnimationDone = false;
    }
    void animDone()
    {
        isAnimationDone = true;
    }
}
