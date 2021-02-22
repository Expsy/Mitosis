using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image startBut;
    public Image exitBut;
    public Image panal;
    public Image Blur;
    public Image name;
    public Image pCon;
    public Image pExit;
    public GameObject pauseMenu;
    public GameObject MainMenu;
    public GameObject winMenu;

    AudioSource audio;



    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeMainMenuOut()
    {
        float elapsedTime = 0;
        float time = 0.5f;

        while (elapsedTime < time)
        {
            Color color = new Color(startBut.color.r, startBut.color.g, startBut.color.b, Mathf.Lerp(1, 0, elapsedTime / time));
            //alpha = Mathf.Lerp(1, 0, elapsedTime / time);
            startBut.color = color;
            exitBut.color = color;
            panal.color = color;
            name.color = color;
            elapsedTime += Time.deltaTime;
             
            yield return null;

        }

        if (true)
        {

            StartCoroutine(FadeBlurOut());
        }

    }

    public void StartFade()
    {
        
        StartCoroutine(FadeMainMenuOut());
    }


    IEnumerator FadeBlurOut()
    {
        float elapsedTime = 0;
        float time = 2f;

        while (elapsedTime < time)
        {
            Color color = new Color(startBut.color.r, startBut.color.g, startBut.color.b, Mathf.Lerp(1, 0, elapsedTime / time));
            //alpha = Mathf.Lerp(1, 0, elapsedTime / time);
            Blur.color = color;
            elapsedTime += Time.deltaTime;

            yield return null;

        }

        if (true)
        {

            MainMenu.SetActive(false);
        }

    }


    IEnumerator FadePauseMenuIn()
    {
        float elapsedTime = 0;
        float time = 0.5f;

        while (elapsedTime < time)
        {
            Color color = new Color(startBut.color.r, startBut.color.g, startBut.color.b, Mathf.Lerp(0, 1, elapsedTime / time));
            //alpha = Mathf.Lerp(1, 0, elapsedTime / time);
            pCon.color = color;
            pExit.color = color;
            elapsedTime += Time.deltaTime;


            yield return null;

        }

        if (true)
        {
        }
    }

    public void StartFadePauseMenuIn()
    {
        AudioListener.volume = 0.5f;
        pauseMenu.SetActive(true);
        StartCoroutine(FadePauseMenuIn());
    }

    IEnumerator FadePauseMenuOut()
    {

        float elapsedTime = 0;
        float time = 0.5f;

        while (elapsedTime < time)
        {
            Color color = new Color(startBut.color.r, startBut.color.g, startBut.color.b, Mathf.Lerp(1, 0, elapsedTime / time));
            //alpha = Mathf.Lerp(1, 0, elapsedTime / time);
            pCon.color = color;
            pExit.color = color;

            elapsedTime += Time.deltaTime;


            yield return null;

        }

        if (true)
        {
            
            AudioListener.volume = 1f;
            pauseMenu.SetActive(false);
        }

    }

    public void StartFadePauseMenuOut()
    {
        StartCoroutine(FadePauseMenuOut());

    }

    public void PlayClick()
    {
        audio.Play();
    }

    public void ActivateWinMenu()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
