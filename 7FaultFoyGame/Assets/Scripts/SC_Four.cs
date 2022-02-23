using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Four : MonoBehaviour
{
    // Info Four
    [System.NonSerialized] public bool isFourReady;
    public float timeToCook;

    // Viennoiserie
    private GameObject viennoiserie;
    private SC_Viennoiserie scViennoiserie;
    public float timeInFour;
    public bool isViennoiserieReady;

    // Player
    public GameObject player;
    private Transform tr;


    void Start()
    {
        isFourReady = true;
        tr = player.GetComponent<Transform>();
    }

    // Update is called once per frame

    void Update()
    {
        // Faire une viennoiserie
        if (!isFourReady && viennoiseriePresent())
        {
            
            timeInFour += Time.deltaTime;
            if (timeInFour < timeToCook)
            {
                scViennoiserie.cookingRate = timeInFour / timeToCook;
                viennoiserie.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                isViennoiserieReady = true;
                scViennoiserie.cookingRate = 1f;
                viennoiserie.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
        else
        {
            isFourReady = true;
            isViennoiserieReady = false;
        }
    }

    public void cookViennoiserie(GameObject viennoiserie)
    {
        this.viennoiserie = viennoiserie;
        scViennoiserie = this.viennoiserie.GetComponent<SC_Viennoiserie>();
        timeInFour = scViennoiserie.cookingRate * timeToCook;
        isFourReady = false;
        viennoiserie.transform.position = transform.position;
    }

    // Verifie si la tasse est sur la machine
    public bool viennoiseriePresent()
    {
        Vector2 posViennoiserie = viennoiserie.transform.position;
        if (Mathf.Sqrt(Mathf.Pow(posViennoiserie.x - transform.position.x, 2) + Mathf.Pow(posViennoiserie.y - transform.position.y, 2)) < 0.1f)
        {
            return true;
        }
        return false;
    }
}
