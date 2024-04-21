using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private new Transform shakeTransform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.05f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        shakeTransform= GetComponent<Transform>();
    }
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    void LateUpdate()
    {
        if (shakeDuration > 0)
        {
            float xPos = shakeTransform.position.x;
            float yPos = shakeTransform.position.y;
            float zPos = shakeTransform.position.z;
            xPos = (Mathf.Lerp(xPos, xPos + Random.insideUnitSphere.x * shakeMagnitude,0.5f));
            yPos = (Mathf.Lerp(yPos, yPos + Random.insideUnitSphere.y * shakeMagnitude, 0.5f));
            transform.position= new Vector3(xPos, yPos,zPos);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
        }
    }
    public void TriggerShake(float shakeamount)
    {
        shakeDuration = shakeamount;
        initialPosition = transform.position;
    }

}
