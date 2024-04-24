using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightflicker : MonoBehaviour
{
    protected new Light2D light;
    public float eachFadeTime = 1f;
    public float fadeWaitTime = 1f;
    public float minLuminosity = 0.3f;
    public float maxLuminosity = 1;

    private void Start()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(fadeInAndOutRepeat(light, eachFadeTime, fadeWaitTime));
    }
    protected virtual IEnumerator fadeInAndOut(Light2D lightToFade, bool fadeIn, float duration)
    {

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }
    protected virtual IEnumerator fadeInAndOutRepeat(Light2D lightToFade, float duration, float waitTime)
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (true)
        {
            //fade out
            yield return fadeInAndOut(lightToFade, false, duration);

            //wait
            yield return waitForXSec;

            //fade-in 
            yield return fadeInAndOut(lightToFade, true, duration);
        }
    }
}
