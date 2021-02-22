using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    CinemachineVirtualCamera cam;
    Colony colony;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        colony = GameObject.FindGameObjectWithTag("MainColony").GetComponent<Colony>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.m_Lens.OrthographicSize = 10 + colony.cellCount * 0.2f;

    }
}
