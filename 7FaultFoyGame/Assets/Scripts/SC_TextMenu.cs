using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SC_TextMenu : MonoBehaviour
{
    // Savoir quel type de Text on a
    public bool isJouer;
    public bool is2J;
    public bool isOption;
    public bool isCredits;
    public bool isBack;
    public bool isQuitter;
    public bool isTuto01;
    public bool isTuto02;
    public Slider slider;


    public float timeOnText;
    public bool inText;

    // Temps qu'il faut attendre sur le text avant le changement de scène
    public float timeBeforeChange;




    void Start()
    {
        timeOnText = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOnText >= timeBeforeChange)
        {
            if (isJouer)
            {
                SceneManager.LoadScene("FoyGame");
            }
            else if (is2J)
            {
                SceneManager.LoadScene("FoyGame2J");
            }
            else if (isQuitter)
            {
                Application.Quit();
            }
            else if (isCredits)
            {
                SceneManager.LoadScene("Credit");
            }
            else if (isBack)
            {
                SceneManager.LoadScene("Menu");
            }
            else if (isTuto01)
            {
                SceneManager.LoadScene("Tuto01");
            }
            else if (isTuto02)
            {
                SceneManager.LoadScene("Tuto02");
            }
        }
        else
        {
            if(inText)
            {
                timeOnText += Time.deltaTime;
                slider.value = timeOnText / timeBeforeChange;
            }
            else{
                timeOnText = 0f;
                slider.value = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            inText = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            inText = false;
        }
    }
}
