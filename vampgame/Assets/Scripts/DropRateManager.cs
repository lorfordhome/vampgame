using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;//1-100
    }
    public List<Drops> drops;

    private void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops= new List<Drops>();
        foreach (Drops rate in drops)
        {
            if (randomNumber<rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        //check if there are possible drops. this system is here so it cant spawn more than one drop
        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
