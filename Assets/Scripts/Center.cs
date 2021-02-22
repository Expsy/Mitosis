using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{

    Vector2 center = Vector2.zero;
    Colony colony;
    public IndicatorController indicatorController;

    private void Awake()
    {
        colony = transform.parent.GetComponentInChildren<Colony>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colony.cellCount>0)
        {
            FindCenter();
            transform.position = center;
        }

    }

    void FindCenter()
    {
        center = Vector2.zero;
        foreach (Transform child in colony.transform)
        {
            if (child.CompareTag("MyCell"))
            {
                center += child.GetComponent<Rigidbody2D>().position;
            }
        }
        center = center / colony.cellCount;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Gas"))
        {
            if (!indicatorController.targetGasses.Contains(collision.gameObject))
            {
                indicatorController.targetGasses.Add(collision.gameObject);
                indicatorController.createIndicator(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gas"))
        {
            if (indicatorController.targetGasses.Contains(collision.gameObject))
            {
                indicatorController.targetGasses.Remove(collision.gameObject);
                Destroy(collision.GetComponent<Gas>().indicator.gameObject);
            }
        }
    }
}
