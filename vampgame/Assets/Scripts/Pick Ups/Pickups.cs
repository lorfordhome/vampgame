using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public float lifespan = 0.5f;
    protected PlayerStats target;
    protected Collector collector;
    protected float speed;
    Vector2 initialPosition;
    float initialOffset;

    [System.Serializable]
    public struct BobbingAnimation
    {
        public float frequency;
        public Vector2 direction;
    }
    public BobbingAnimation bobbingAnimation = new BobbingAnimation
    {
        frequency = 2f,
        direction = new Vector2(0, 0.3f)
    };

    protected virtual void Start()
    {
        initialPosition=transform.position;
        initialOffset = Random.Range(0, bobbingAnimation.frequency);//they all start at a slightly different frame. looks cleaner
        collector=FindObjectOfType<Collector>();
    }

    protected virtual void Update()
    {
        if (target)
        {
            Vector2 distance = target.transform.position - transform.position;
            if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
            {
                transform.position += (Vector3)distance.normalized * speed * Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //animation
            transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin((Time.time +initialOffset)* bobbingAnimation.frequency);
        }
    }

    public virtual bool Collect(PlayerStats target,float speed, float lifespan = 0)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            collector.SendMessage("StartAudio");
            if (lifespan > 0) this.lifespan = lifespan;
            Destroy(gameObject,Mathf.Max(0.01f,this.lifespan));
            return true;
        }
        return false;
    }
}
