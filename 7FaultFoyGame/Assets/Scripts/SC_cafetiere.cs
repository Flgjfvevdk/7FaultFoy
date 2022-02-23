using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_cafetiere : MonoBehaviour
{
    // Info cafetiere
    [System.NonSerialized] [Range(0f, 1f)] public float waterRate;
    [System.NonSerialized] [Range(0f, 1f)] public float coffeeRate;
    [System.NonSerialized] public bool isMachineReady;
    public float timeToMakeCoffee;

    // Tasse
    private GameObject tasse;
    private SC_Tasse scTasse;
    public float timeOnMachine;
    public bool isCoffeeReady;

    // Player
    public GameObject player;
    private Transform tr;


    void Start()
    {
        coffeeRate = 1f;
        waterRate = 1f;
        isMachineReady = true;
        tr = player.GetComponent<Transform>();
    }

    // Update is called once per frame

    void Update()
    {
        // Faire un café (la machine )
        if (!isMachineReady && tassePresent())
        {
            // Il ne faut pas qu'il y ai de sirop pour faire un café
            if (!scTasse.isSirop)
            {
                timeOnMachine += Time.deltaTime;
                scTasse.isCafe = true;
                if (timeOnMachine < timeToMakeCoffee)
                {
                    scTasse.fillingRate = timeOnMachine / timeToMakeCoffee;
                    tasse.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    isCoffeeReady = true;
                    scTasse.fillingRate = 1f;
                    tasse.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
        else
        {
            isMachineReady = true;
        }
    }

    public void makeCoffee(GameObject tasseLifted)
    {
        this.tasse = tasseLifted;
        scTasse = tasse.GetComponent<SC_Tasse>();
        timeOnMachine = scTasse.fillingRate * timeToMakeCoffee;
        isMachineReady = false;
        tasseLifted.transform.position = transform.position;
    }

    // Verifie si la tasse est sur la machine
    public bool tassePresent()
    {
        Vector2 posTasse = tasse.transform.position;
        if (Mathf.Sqrt(Mathf.Pow(posTasse.x - transform.position.x, 2) + Mathf.Pow(posTasse.y - transform.position.y, 2)) < 0.1f)
        {
            return true;
        }
        return false;
    }

    // // Pour l'instant je pense que c'est mieux de laisser le script du player detecter les machines proche quand il fait des actions
    // public bool playerClose()
    // {
    //     if (Mathf.Sqrt((tr.position.x - transform.position.x) * (tr.position.x - transform.position.x) + (tr.position.y - transform.position.y) * (tr.position.y - transform.position.y)) < distance)
    //     {
    //         return true;
    //     }
    //     return false;
    // }
}
