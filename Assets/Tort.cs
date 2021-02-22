using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tort : MonoBehaviour
{

    Vector3 swimDirection;
    Center center;
    Rigidbody2D rbody;
    float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        center = FindObjectOfType<Center>();
        rbody = GetComponent<Rigidbody2D>();
        TargetCenter();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.velocity = swimDirection.normalized * speed;

        float angle = Mathf.Atan2(swimDirection.y, swimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if ((transform.position - center.transform.position).magnitude > 50f)
        {
            Destroy(gameObject);
        }
        //Debug.Log(center.transform.position);
    }

    private void TargetCenter()
    {
        float xf = Random.Range(-0.3f, 0.3f);
        float yf = Random.Range(-0.3f, 0.3f);

        swimDirection = (center.transform.position - transform.position).normalized + new Vector3(xf, yf, 0);

    }
}
