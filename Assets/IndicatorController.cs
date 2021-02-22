using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{

    public List<GameObject> targetGasses;
    public GameObject gasIndicatorPrefab;
    public GameObject indicatorCanvas;


    private void Awake()
    {
        targetGasses = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createIndicator(GameObject targetObject)
    {
        GameObject createdIndicator = Instantiate(gasIndicatorPrefab, targetObject.transform.position, Quaternion.identity) as GameObject;
        createdIndicator.transform.SetParent(indicatorCanvas.transform);
        createdIndicator.GetComponent<TargetIndicator>().target = targetObject;
        targetObject.GetComponent<Gas>().indicator = createdIndicator;
    }
}
