using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Material myMaterial;
    public Image blurImage;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.GetDefaultCanvasMaterial().shader = myMaterial.shader; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
