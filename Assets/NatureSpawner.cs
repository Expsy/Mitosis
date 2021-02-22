using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureSpawner : MonoBehaviour
{
    public GameObject TortPrefab;
    public GameObject KoiBlackPrefab;

    Center center;
    float TortspawnDistance = 40f;
    float BlackKoispawnDistance = 30f;

    public bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        center = FindObjectOfType<Center>();
        InvokeRepeating("SpawnTort", 1, 15);
        InvokeRepeating("SpawnBlackKoi", 1, 7);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnTort()
    {
        if (canSpawn)
        {
            Vector2 pos = new Vector2(center.transform.position.x, center.transform.position.y) + Random.insideUnitCircle.normalized * TortspawnDistance;
            GameObject spawnedTort = Instantiate(TortPrefab, pos, Quaternion.identity) as GameObject;
        }
    }

    void SpawnBlackKoi()
    {
        if (canSpawn)
        {
            Vector2 pos = new Vector2(center.transform.position.x, center.transform.position.y) + Random.insideUnitCircle.normalized * BlackKoispawnDistance;
            GameObject spawnedKoiBlack = Instantiate(KoiBlackPrefab, pos, Quaternion.identity) as GameObject;
        }
    }
}
