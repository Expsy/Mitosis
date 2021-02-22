using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;

    public GameObject target;
    private Vector3 targetPosition;
    private RectTransform indicatorRectTransform;
    float borderPadding = 100f;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        uiCamera = Camera.main;
        targetPosition = target.transform.position;
        indicatorRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = target.transform.position;

        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (targetPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        indicatorRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width - borderPadding || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height - borderPadding;
        

        if (isOffScreen)
        {
            transform.localScale = new Vector3(1, 1, 1);


            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderPadding, Screen.width - borderPadding);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderPadding, Screen.height - borderPadding);

            Vector3 indicatorWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            indicatorRectTransform.position = indicatorWorldPosition;
            indicatorRectTransform.localPosition = new Vector3(indicatorRectTransform.localPosition.x, indicatorRectTransform.localPosition.y, 0);
        }
        else
        {
            transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
