using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public GameObject point;
    Vector3 axis = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point.transform.position, axis, Time.deltaTime * 50);
    }
}
