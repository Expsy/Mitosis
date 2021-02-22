using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    float contactTime = 0;
    [SerializeField]
    float size;
    public Cell targetCell = null;
    Colony targetColony = null;
    float consumeSpeed = 0.1f;
    float growSpeed = 0.05f;
    float feedSpeed = 0.5f;
    SpriteRenderer[] glowSprite;
    Color defaultColor;
    Color lowAlpha;
    Vector3 firstScale;
    float firstSize;
    bool canGrow = true;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        glowSprite = GetComponentsInChildren<SpriteRenderer>();
        defaultColor = glowSprite[1].color;
        lowAlpha = defaultColor;
        firstScale = transform.localScale;
        firstSize = firstScale.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        size = transform.localScale.magnitude;

        Color tmp = glowSprite[1].color;
        tmp.a = transform.localScale.magnitude / firstScale.magnitude;
        glowSprite[1].color = tmp;


        if (size <= 0.04f)
        {
            Destroy(gameObject);
        }

        GetBigger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyCell"))
        {
            audio.Play();
            if (targetColony == null)
            {
                TargetAColony(collision);
                TargetACell();
            }

            if (targetColony != null)
            {

                if (collision.gameObject.transform.parent.GetComponent<Colony>() == targetColony)
                {
                    CancelInvoke("LoseTarget");
                }
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!TimelineControl.playing)
        {
            if (collision.CompareTag("MyCell"))
            {
                contactTime += Time.deltaTime;
                if (contactTime > 0.2f && size > 0.04f && targetCell)
                {
                    transform.localScale -= Vector3.one * consumeSpeed * Time.deltaTime;
                    targetCell.transform.localScale += Vector3.one * feedSpeed * Time.deltaTime;

                }
                else
                {
                    TargetACell();
                }
            }
        }
       
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("MyCell"))
        {
            audio.Stop();
            Invoke("LoseTarget", 5);

        }
    }


    public void TargetACell()
    {
        int i = Random.Range(0, targetColony.cellCount);
        targetCell = targetColony.transform.GetChild(i).GetComponent<Cell>();

    }

    public void TargetAColony(Collider2D collidingColony)
    {
        targetColony = collidingColony.gameObject.transform.parent.GetComponent<Colony>();
    }

    void LoseTarget()
    {
        targetColony = null;
        targetCell = null;
    }

    void GetBigger()
    {
        if (targetColony == null && canGrow && size < firstSize)
        {
            transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        }
    }
}
