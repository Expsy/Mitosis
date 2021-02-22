using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public Rigidbody2D rBody;
    Center center;
    public GameObject indicator;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.FindObjectOfType<Center>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimelineControl.playing)
        {
            GetComponent<CircleCollider2D>().enabled = false;
        }
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
