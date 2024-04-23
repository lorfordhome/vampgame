using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LightRing : MonoBehaviour
{

    //public GameObject Lantern; 
    public float shrinkRate = 0.05f; //how fast the light shrinks
    public float growRate = 0.1f; //how fast the light can grow
    protected new Light2D light;
    PlayerStats player;
    private new AudioSource audio;

    public Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f); // min size is small enough that you cant see it
    public Vector3 maxSize = new Vector3(2.5f, 2.5f, 2.5f); //set max size as slightly bigger than initial size
    public float eachFadeTime = 1f;
    public float fadeWaitTime = 1f;
    public float minLuminosity = 0.2f;
    public float maxLuminosity = 1;
    bool isShrinking = false;
    bool nearDeath = false;


    // Update is called once per frame
    private void Start()
    {
        light = GetComponent<Light2D>();
        player = FindObjectOfType<PlayerStats>();
        audio = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (transform.localScale != minSize)
            {
                ShrinkLight(shrinkRate);

            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (transform.localScale != maxSize)
            {
                GrowLight(growRate);

            }
        }
        ShrinkLight(shrinkRate);
        if (transform.localScale.x <= (maxSize.x / 2))
        {
            if (!isShrinking)
            {
                StartCoroutine(fadeInAndOutRepeat(light, eachFadeTime, fadeWaitTime));
            }
        }
        else if (nearDeath)
        {
            nearDeath = false;
        }

        if (transform.localScale.x <= (maxSize.x / 10) && (!nearDeath))
        {
            audio.Play();
            nearDeath = true;
        }
        if (transform.localScale.x <= minSize.x)
        {
            player.TakeDamage(20f);
        }
    }
    public void ShrinkLight(float shrink)
    {
        transform.localScale -= new Vector3(shrinkRate, shrinkRate, shrinkRate);
        transform.localScale = new Vector3(
               Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
               Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
               Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));
    }
    public void GrowLight(float growth)
    {
        //grows the lanterns scale uniformly
        transform.localScale += new Vector3(growth, growth, growth);

        //clamping the maximum size
        transform.localScale = new Vector3(
        Mathf.Clamp(transform.localScale.x, minSize.x, maxSize.x),
        Mathf.Clamp(transform.localScale.y, minSize.y, maxSize.y),
        Mathf.Clamp(transform.localScale.z, minSize.z, maxSize.z));
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
        isShrinking = true;
        Debug.Log("FLICKERING");
        WaitForSeconds waitForXSec = new WaitForSeconds(waitTime);

        while (transform.localScale.x <= (maxSize.x / 2))
        {
            //Fade out
            yield return fadeInAndOut(lightToFade, false, duration * (transform.localScale.x / maxSize.x));
            Debug.Log(transform.localScale.x / maxSize.x);

            //Wait
            yield return new WaitForSeconds(waitTime * (transform.localScale.x / maxSize.x));

            //Fade-in 
            yield return fadeInAndOut(lightToFade, true, duration * (transform.localScale.x / maxSize.x));
        }
        isShrinking = false;
        Debug.Log("FLICKERING DONE?");
    }
}

