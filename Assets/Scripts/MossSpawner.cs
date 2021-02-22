using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossSpawner : MonoBehaviour
{
    public GameObject mossPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnMossBatch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnMossBatch()
    {
        foreach (Transform mossSlot in transform)
        {
            if (mossSlot.childCount < 1)
            {
                GameObject spawnedMoss = Instantiate(mossPrefab, mossSlot.transform.position, Quaternion.identity, mossSlot.transform) as GameObject;
                float i = Random.Range(6, 13);
                spawnedMoss.transform.localScale *= i / 10f;
            }
        }
    }
}
