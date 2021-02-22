using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Colony : MonoBehaviour
{
    public float moveSpeed = 5;
    public int cellCount = 0;
    public Center center;
    public int illCells = 0;
    public GameObject losePanel;
    public Vector2 generalSpeed;
    bool firstLight = false;
    bool secondLight = false;
    bool thirdLight = false;
    bool fourthLight = false;

    public enum state
    {
        Move,
        Edit
    }

    public state myState;

    private void Awake()
    {
        center = transform.parent.GetComponentInChildren<Center>();
        myState = state.Move;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateCellCount();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateCellCount();
        FindIllCells();
        LoseGame();
        //Debug.Log(illCells);
        //Debug.Log(cellCount);
        LightTheCell();
    }

    private void FixedUpdate()
    {
        
    }

    public void MoveCells(RaycastHit2D hit)
    {
        if (myState == state.Move)
        {
            foreach (Transform child in transform)
            {
                Cell cell = child.GetComponent<Cell>();
                cell.Move(moveSpeed, hit);
                generalSpeed = child.GetComponent<Rigidbody2D>().velocity;
            }
        }

        
    }

    void LoseGame()
    {
        if (cellCount <= 0 || illCells == cellCount)
        {
            losePanel.SetActive(true);
        }
    }

    void FindIllCells()
    {
        illCells = 0;

        foreach (Transform child in transform)
        {

            Cell cell = child.GetComponent<Cell>();
            if (cell.isInfected)
            {
                illCells += 1;
            }
        }
    }

    public void UpdateCellCount()
    {
        cellCount = transform.childCount;
    }

    void LightTheCell()
    {
        if (cellCount >3 && !firstLight)
        {
            transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            firstLight = true;
        }
        else if (cellCount > 7 && !secondLight)
        {
            transform.GetChild(7).GetChild(0).gameObject.SetActive(true);
            secondLight = true;
        }
        else if (cellCount > 11 && !thirdLight)
        {
            transform.GetChild(11).GetChild(0).gameObject.SetActive(true);
            thirdLight = true;
        }
        else if (cellCount > 15 && !fourthLight)
        {
            transform.GetChild(15).GetChild(0).gameObject.SetActive(true);
            fourthLight = true;
        }


    }
}
