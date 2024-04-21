using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<GameObject> spawnPoints;
    public List<GameObject> prefabs;
    public float spawnFrequency;
    public int pickupQuota;
    public int currentPickups;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(respawnPickups());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator respawnPickups()
    {
        WaitForSeconds waitForXSec = new WaitForSeconds(spawnFrequency);
        while (true)
        {
            CountPickups();
            if (currentPickups< pickupQuota)
            {
                SpawnPickups();
            }
            yield return new WaitForSecondsRealtime(spawnFrequency);
        }
    }
    void CountPickups()
    {
        currentPickups = 0;
        foreach (var spawnpoint in spawnPoints)
        {
            foreach (Transform transform in spawnpoint.transform)
            {
                if (transform.CompareTag("pickup"))
                {
                    ++currentPickups;
                }
            }
        }
        Debug.Log(currentPickups + " pickups active");
    }
    void SpawnPickups()
    {
        List<GameObject> possibleSpawns = new List<GameObject>( spawnPoints);
        for (int i = 0;i<pickupQuota;i++)
        {
            GameObject sp = possibleSpawns[Random.Range(0,possibleSpawns.Count)]; 
            possibleSpawns.Remove(sp);
            int rand = Random.Range(0, prefabs.Count);
            GameObject prop=Instantiate(prefabs[rand],sp.transform.position,Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
