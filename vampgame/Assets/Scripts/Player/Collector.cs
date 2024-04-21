using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D detector;
    public float pullSpeed;
    public AudioSource adSource;
    public AudioClip[] adClips;
    private int adCount = 0;

    public void StartAudio()
    {
        StartCoroutine(PlayAudio());
    }
    IEnumerator PlayAudio()
    {
        adSource.clip = adClips[adCount];
        yield return new WaitForSeconds(0.2f);
        adSource.Play();
        adCount++;
        if (adCount > 4)
        {
            adCount = 0;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        detector = GetComponent<CircleCollider2D>();
        adSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        detector.radius = player.currentMagnet;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
            //checks if the other object is a collectible
            if (col.gameObject.TryGetComponent(out Pickups collectible))
            {
                collectible.Collect(player,pullSpeed);
            }

    }
}
