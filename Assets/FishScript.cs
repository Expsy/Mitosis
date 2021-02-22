using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    GameObject target;
    Vector2 swimDirection;
    Rigidbody2D rbody;
    Animator anim;
    bool dissolveTrigger = false;
    public Center center;
    float normalSpeed = 2;
    float actSpeed = 3;
    AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        center = FindObjectOfType<Center>();

        TargetCenter();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (target)
        {
            anim.SetBool("Acting", true);
            SwimToTarget();
        }
        else
        {
            anim.SetBool("Acting", false);
            SwimIdle();
        }
        float angle = Mathf.Atan2(swimDirection.y, swimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //Debug.Log(swimDirection);

        if ((transform.position - center.transform.position).magnitude > 50f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyCell") && !target)
        {
            target = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Thrown"))
        {
            target = collision.gameObject;
        }
    }

    void SwimToTarget()
    {

        float distance = Vector2.Distance(target.transform.position, transform.position);
        swimDirection = target.transform.position - transform.position;
        
        if (distance > 1f)
        {
            rbody.velocity = swimDirection.normalized * actSpeed;
        }
        else if (distance <= 1f)
        {
            target.GetComponent<Cell>().ReleaseCell();
            target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            rbody.velocity = Vector2.zero;

            if (!dissolveTrigger)
            {
                Invoke("DissolveTarget", 3);
                dissolveTrigger = true;

                audio.Play();
            }
        }
    }

    private void TargetCenter()
    {
        float xf = Random.Range(-0.3f, 0.3f);
        float yf = Random.Range(-0.3f, 0.3f);

        swimDirection = (center.transform.position - transform.position).normalized + new Vector3(xf, yf, 0);
    }

    void SwimIdle()
    {
        rbody.velocity = swimDirection * normalSpeed;
    }

    void DissolveTarget()
    {
        target.GetComponent<DissolveEffect>().BeginDissolving();
        dissolveTrigger = false;
        target = null;
        TargetCenter();
    }
}
