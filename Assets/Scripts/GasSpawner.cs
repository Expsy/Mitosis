using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    public GameObject GasPrefab;
    GameObject center;
    float gasSpeed = 10f;
    float gasDistance = 40f;
    float growRate = 0.1f;
    int growIteration = 1;
    public bool canSpawn;

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindObjectOfType<Center>().gameObject;
        InvokeRepeating("SpawnGas", 5, 12);
        InvokeRepeating("ChangeGrowRate", 30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnGas()
    {
        if (canSpawn)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle.normalized * gasDistance;
            GameObject spawnedGas = Instantiate(GasPrefab, pos, Quaternion.identity) as GameObject;
            Debug.Log((center.transform.position - spawnedGas.transform.position).normalized);
            spawnedGas.transform.localScale *= 1 + (growIteration * growRate);
            var sh = spawnedGas.GetComponentInChildren<ParticleSystem>().shape;
            sh.radius *= 1 + (growIteration * growRate);
            spawnedGas.GetComponent<Gas>().rBody.velocity = (center.transform.position - spawnedGas.transform.position).normalized * gasSpeed;
        }
    }

    void ChangeGrowRate()
    {
        growRate += 0.1f;
    }
}
