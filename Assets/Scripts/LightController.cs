using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightController : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    Center center;
    float intensity;

    private void Awake()
    {
        center = FindObjectOfType<Center>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        intensity = Mathf.Clamp(1 - center.transform.position.y / 40, 0.005f ,1);
        globalLight.intensity = intensity;
    }
}
