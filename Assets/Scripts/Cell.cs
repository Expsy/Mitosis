using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cell : MonoBehaviour
{

    public GameObject cellPrefab;
    Rigidbody2D rBody;
    CircleCollider2D myCollider;
    Center center;
    public Colony colony;
    [SerializeField]
    float currentSpeed;
    float speedModifier;
    public float speedRandomizerRate = 0.3f;
    float deceleration = 10;
    public bool isBeingControlled = false;
    public bool isDividable = false;
    public bool isInfected = false;
    public bool isShrinking = false;
    public float size;
    public Color defaultColor;
    public Color currentColor;

    float maxSizeMultiplier = 4f;
    float divideSizeMultiplier = 2f;
    Vector3 defaultSize;

    public Transform heldObjectParent;
    public Transform PropsParent;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isDragable = true;
    public bool isBeignDragged = false;
    public float boingFactor = 0;
    Vector3 boingVector;


    Vector2 myLastMousePosWorld = Vector2.zero;
    Vector2 myCurrentMousePosPixel = Vector2.zero;
    Vector2 myCurrentMousePosWorld = Vector2.zero;
    float launchForce = 200f;
    Vector2 launchDirection = Vector2.zero;

    public AudioClip divide;
    public AudioClip death;
    public AudioClip poisonned;
    public AudioClip eat;


    AudioSource audioSource;

    public enum state
    {
        Normal,
        Infected,
        Dividable,
        Seperated
    }

    state cellState;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rBody = GetComponent<Rigidbody2D>();
        colony = GetComponentInParent<Colony>();
        center = colony.center;
        myCollider = GetComponent<CircleCollider2D>();
        GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
    }

    // Start is called before the first frame update
    void Start()
    {
        heldObjectParent = GameObject.FindGameObjectWithTag("HeldObjectsParent").transform;
        PropsParent = GameObject.FindGameObjectWithTag("PropsParent").transform;
        cellState = state.Normal;
        defaultColor = GetComponent<SpriteRenderer>().color;
        cellPrefab = (GameObject) Resources.Load("Cell");
        defaultSize = transform.localScale;
        transform.position += new Vector3(0.1f, 0.1f, 0);
        boingVector = new Vector3(boingFactor, boingFactor, 0);
        StartBoingUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (colony.cellCount <= 1)
        {
            isDragable = false;
        }

        CellStateControl();
        ColorStateControl();
        CheckIfDividable();
        InputCheck();

        center = colony.center;
        size = transform.localScale.magnitude;

        Explode();
        CalculateLaunch();



    }

    private void FixedUpdate()
    {

        if (!isBeingControlled && rBody.velocity != Vector2.zero && !isBeignDragged)
        {
            Gather();
        }
    }

    public void Move(float speed, RaycastHit2D hit)
    {
        isBeingControlled = true;
        Vector2 moveVelocity = (hit.point - rBody.position).normalized;
        currentSpeed = speed + speedModifier;
        rBody.AddForce(moveVelocity * currentSpeed);
    }

    void Gather()
    {
        rBody.AddForce((new Vector2(center.transform.position.x, center.transform.position.y) - rBody.position)*2);

    }

    void InputCheck()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RandomizeModifier(speedModifier);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isBeingControlled = false;
        }

    }

    void RandomizeModifier(float speed)
    {
        speedModifier = Random.Range(-speedRandomizerRate, speedRandomizerRate);
    }

    void CheckIfDividable()
    {
        if (size >= defaultSize.magnitude * divideSizeMultiplier && !isInfected && !isShrinking)
        {
            isDividable = true;
        }
        else
        {
            isDividable = false;
        }
    }

    public void Divide()
    {
        isShrinking = true;

        Debug.Log(isShrinking);
        audioSource.clip = divide;
        audioSource.Play();
        StartCoroutine(Shrink());
        isDragable = true;
    }

    IEnumerator Shrink()
    {
        float elapsedTime = 0;
        float time = 0.5f;
        Vector3 currentSize = transform.localScale;

        while (elapsedTime < time)
        {
            
            transform.localScale = Vector3.Lerp(currentSize, defaultSize, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        if (true)
        {
            isShrinking = false;
            Instantiate(cellPrefab, transform.position, Quaternion.identity, colony.transform);
            StartBoingUp();
        }

    }

    IEnumerator BoingUp()
    {

        float elapsedTime = 0;
        float time = 0.1f;
        Vector3 currentSize = transform.localScale;

        while (elapsedTime < time)
        {

            transform.localScale = Vector3.Lerp(currentSize, currentSize + boingVector, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        if (true)
        {
            StartCoroutine(BoingDown());
        }

    }

    public void StartBoingUp()
    {
        StartCoroutine(BoingUp());
    }

    IEnumerator BoingDown()
    {
        float elapsedTime = 0;
        float time = 0.1f;
        Vector3 currentSize = transform.localScale;

        while (elapsedTime < time)
        {

            transform.localScale = Vector3.Lerp(currentSize, currentSize - boingVector, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

    }

    void Explode()
    {
        if (size > defaultSize.magnitude * maxSizeMultiplier)
        {
            Destroy(gameObject);
        }
    }

    public void Infection()
    {

    }


    IEnumerator  InfectionColorChange()
    {
        float elapsedTime = 0;
        float time = 6f;

        while (elapsedTime < time)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, Color.green, elapsedTime/time);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        if (colony)
        {
            InfectTargetCell();
        }

    }
    void ColorStateControl()
    {
        switch (cellState)
        {
            case state.Infected:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case state.Dividable:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            default:
                GetComponent<SpriteRenderer>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b);
                break;
        }
    }

    void CellStateControl ()
    {
        if (isInfected)
        {
            cellState = state.Infected;
        }
        else if (isDividable && !isInfected)
        {
            cellState = state.Dividable;
        }
        else
        {
            cellState = state.Normal;
        }
    }

    Cell InfectionSpreadTarget()
    {
        Cell bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in colony.transform)
        {
            Cell targetCell = potentialTarget.GetComponent<Cell>();
            if (!targetCell.isInfected)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = targetCell;
                }
            }
        }
        return bestTarget;
    }

    public void GetInfected()
    {
        audioSource.clip = poisonned;
        audioSource.Play();
        isInfected = true;
        currentColor = GetComponent<SpriteRenderer>().color;
        StartCoroutine(InfectionColorChange());
    }

    void InfectTargetCell()
    {
        if (InfectionSpreadTarget())
        {
            InfectionSpreadTarget().GetInfected();

        }
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (isDragable)
        {
            InputController.dragElapsedTime += Time.deltaTime;
            if (InputController.dragElapsedTime > InputController.dragTimeTreshold)
            {
                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
                //transform.position = curPosition;
                rBody.velocity =  (curPosition - transform.position) * 2;
                //transform.Translate((curPosition - transform.position) * Time.deltaTime * 3);

                transform.tag = "NotMyCell";
                isBeignDragged = true;
                transform.parent = heldObjectParent;
                colony.myState = Colony.state.Edit;
                colony.UpdateCellCount();
                gameObject.layer = 9;
            }
        }

    }

    private void OnMouseUp()
    {
        if (isBeignDragged)
        {
            isBeignDragged = false;
            gameObject.layer = 0;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, myCollider.radius);

            foreach (Collider2D item in colliders)
            {
                if (item.CompareTag("MyCell"))
                {
                    transform.tag = "MyCell";
                    transform.parent = colony.transform;
                    colony.UpdateCellCount();
                    colony.myState = Colony.state.Move;

                    return;
                }
                else
                {
                    rBody.AddForce(launchDirection * launchForce);
                    ReleaseCell();
                }

            }


        }
        
    }

    public void ReleaseCell()
    {
        transform.parent = PropsParent;
        if (colony)
        {
            colony.UpdateCellCount();
            colony.myState = Colony.state.Move;
        }

        isDragable = false;
        colony = null;
        tag = "Thrown";
        if (cellState == state.Infected)
        {
            GetComponent<InfectedCell>().isCreated = false;
            GetComponent<InfectedCell>().enabled = true;
        }
        GetComponent<Cell>().enabled = false;
    }

    void CalculateLaunch()
    {
        myCurrentMousePosPixel = Input.mousePosition;
        myCurrentMousePosWorld = Camera.main.ScreenToWorldPoint(myCurrentMousePosPixel);

        launchDirection = (myCurrentMousePosWorld - myLastMousePosWorld).normalized;
        myLastMousePosWorld = myCurrentMousePosWorld;
    }
}
