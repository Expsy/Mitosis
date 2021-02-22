using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;


public class TimelineControl : MonoBehaviour
{

    public PlayableDirector timeline;
    public static bool playing = false;
    public GameObject center;
    public CinemachineVirtualCamera cam;
    bool camGrow = false;
    public Whale whale;
    public InputController inputController;
    public UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (center.transform.position.y >= 100 && !playing)
        {
            timeline.Play();
            playing = true;
            camGrow = true;
            whale.transform.position = center.transform.position + new Vector3(-44, 115, 0);
            whale.canMove = true;
            inputController.cinematic = true;
            Invoke("Win", 20);
        }

        if (camGrow)
        {
            cam.GetComponent<CameraController>().enabled = false;
            if (cam.m_Lens.OrthographicSize <= 24)
            {
                cam.m_Lens.OrthographicSize += 1 * Time.deltaTime;

            }
            if (cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight < 0.6f)
            {
                cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight += 0.1f * Time.deltaTime;
            }
        }
    }

    void Win()
    {
        manager.ActivateWinMenu();
    }
}
