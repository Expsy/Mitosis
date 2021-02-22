using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject FishPrefab;
    public Center center;
    float spawnDistance = 30f;
    public bool canSpawn;
    public float depth;
    public int dangerLevel;
    public int spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        depth = center.transform.position.y;
        //center = FindObjectOfType<Center>();
        //InvokeRepeating("SpawnFish", 1, 5);
        StartCoroutine(SpawnFish());
    }

    // Update is called once per frame
    void Update()
    {
        depth = center.transform.position.y;

        if (depth <= -70)
        {
            dangerLevel = 0;
        }
        else if (depth > -70 && depth <= 0)
        {
            dangerLevel = 1;
        }
        else if (depth > 0 && depth <= 30)
        {
            dangerLevel = 2;
        }
        else if (depth > 30)
        {
            dangerLevel = 3;
        }


        if (dangerLevel == 0)
        {
            spawnRate = 15;
        }
        else if (dangerLevel == 1)
        {
            spawnRate = 10;
        }
        else if (dangerLevel == 2)
        {
            spawnRate = 5;
        }
        else if (dangerLevel == 3)
        {
            spawnRate = 2;
        }
    }

    //void SpawnFish()
    //{
    //    if (canSpawn)
    //    {
    //        Vector2 pos = new Vector2(center.transform.position.x, center.transform.position.y) + Random.insideUnitCircle.normalized * spawnDistance;
    //        GameObject spawnedKoi = Instantiate(FishPrefab, pos, Quaternion.identity) as GameObject;
    //        float f = Random.Range(1f, 1.5f);
    //        spawnedKoi.transform.localScale *= f;
    //    }
    //}

    IEnumerator SpawnFish()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(spawnRate);

            Vector2 pos = new Vector2(center.transform.position.x, center.transform.position.y) + Random.insideUnitCircle.normalized * spawnDistance;
            GameObject spawnedKoi = Instantiate(FishPrefab, pos, Quaternion.identity) as GameObject;
            float f = Random.Range(1f, 1.5f);
            spawnedKoi.transform.localScale *= f;
        }



    }
}
