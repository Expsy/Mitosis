using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedCell : MonoBehaviour
{

    GameObject infectedCellPrefab;
    public bool isCreated = true;
    float launchSpeed = 500f;

    private void OnEnable()
    {
        if (!isCreated)
        {
            InvokeRepeating("Divide", 1, 5);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float xf = Random.Range(-0.5f, 0.5f);
        float yf = Random.Range(-0.5f, 0.5f);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(xf, yf));

        infectedCellPrefab = (GameObject)Resources.Load("InfectedCell");

        if (!isCreated)
        {
            Invoke("DivideWell", 22);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    void Divide()
    {
        GameObject spawnedCell = Instantiate(infectedCellPrefab, transform.position, Quaternion.identity);
        spawnedCell.transform.SetParent(transform);
    }

    void DivideWell()
    {
        GameObject spawnedCell = Instantiate(infectedCellPrefab, transform.position, Quaternion.identity);
        spawnedCell.transform.SetParent(transform);
        spawnedCell.GetComponent<InfectedCell>().isCreated = false;

        Launch();
    }

    void Launch()
    {
        Debug.Log("launched");
        float xf = Random.Range(-1f, 1f);
        float yf = Random.Range(-1f, 1f);
        GetComponent<CircleCollider2D>().enabled = false;
        Vector2 launchDirection = new Vector2(xf, yf).normalized;

        GetComponent<Rigidbody2D>().AddForce(launchDirection * launchSpeed);
        Invoke("EnableCollider", 0.5f);
    }

    void EnableCollider()
    {
        GetComponent<CircleCollider2D>().enabled = true ;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyCell"))
        {
            Cell cell = collision.GetComponent<Cell>();
            if (!cell.isInfected)
            {
                cell.GetInfected();

            }
        }
    }
}
