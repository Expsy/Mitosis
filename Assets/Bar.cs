using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{

    public Image loadingBar;
    public Colony colony;
    int level = 4;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.fillAmount = colony.cellCount / level;
        Debug.Log(loadingBar.fillAmount);
    }
}
