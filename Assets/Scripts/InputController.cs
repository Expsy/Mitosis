using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Colony mainColony;
    public bool canDrag = false;
    public static float dragElapsedTime = 0;
    public static float dragTimeTreshold = 0.05f;
    bool timeTrigger = false;
    public UIManager ui;
    bool pause = false;
    public bool cinematic = false;
    public GameObject Organism;






    private void Awake()
    {
        mainColony = GameObject.FindGameObjectWithTag("MainColony").GetComponent<Colony>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        InputCheck();

    }

    public void InputCheck()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            ui.StartFadePauseMenuIn();
            pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            ui.StartFadePauseMenuOut();
            pause = false;
        }

        if (!cinematic)
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;

                Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("StaticObjects"))
                {
                    mainColony.MoveCells(hit);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 10;

                Vector2 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
                RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);

                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("MyCell"))
                {
                    Cell hitCell = hitObject.GetComponent<Cell>();

                    if (hitCell.isDividable && !hitCell.isShrinking)
                    {
                        hitCell.Divide();
                        mainColony.UpdateCellCount();
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {

                dragElapsedTime = 0;
            }
        }
        else if (cinematic)
        {
            Organism.transform.Translate(Vector2.up * Time.deltaTime);
        }
    }
}
