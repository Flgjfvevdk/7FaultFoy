using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_DistributeurSirop : MonoBehaviour
{
     // Info cafetiere
    [System.NonSerialized] public bool isDistributeurReady;
    public float timeToMakeSirop;

    // Tasse
    private GameObject tasse;
    private SC_Tasse scTasse;
    public float timeOnMachine;
    public bool isSiropReady;

    // Player
    public GameObject player;
    private Transform tr;

    public GameObject ptExcl;
    private GameObject ptExclamationReel;
    public Transform posPtExcl;

    public Transform posVerre;
    void Start()
    {
        isDistributeurReady = true;
        tr = player.GetComponent<Transform>();
    }

    // Update is called once per frame

    void Update()
    {
        // Faire un café (la machine )
        if (!isDistributeurReady && tassePresent())
        {
            // Il ne faut pas qu'il y ai de café pour faire un sirop
            if(!scTasse.isCafe && scTasse.fillingSirop < 1){
                timeOnMachine += Time.deltaTime;
                scTasse.isSirop = true;
                if (timeOnMachine < timeToMakeSirop)
                {
                    scTasse.fillingSirop = timeOnMachine / timeToMakeSirop;
                    //tasse.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                else
                {
                    isSiropReady = true;
                    scTasse.fillingSirop = 1f;
                    //tasse.GetComponent<SpriteRenderer>().color = Color.magenta;
                }
            }
        }
        else
        {
            isDistributeurReady = true;
            isSiropReady = false;
            GetComponent<Animator>().SetBool("isActif", false);
        }


        if (isSiropReady && ptExclamationReel == null)
        {
            ptExclamationReel = Instantiate(ptExcl, posPtExcl.position, Quaternion.identity);
            GetComponent<Animator>().SetBool("isActif", false);
        } else if(!isSiropReady && ptExclamationReel != null)
        {
            Destroy(ptExclamationReel);
        }
    }

    public void makeSirop(GameObject tasseLifted)
    {
        this.tasse = tasseLifted;
        scTasse = tasse.GetComponent<SC_Tasse>();
        timeOnMachine = scTasse.fillingSirop * timeToMakeSirop;
        isDistributeurReady = false;
        tasseLifted.transform.position = posVerre.position;

        if(scTasse.fillingSirop >= 1)
        {
            isSiropReady = true;
        }
        if (!scTasse.isCafe)
        {
            GetComponent<Animator>().SetBool("isActif", true);
        }
        
    }

    // Verifie si la tasse est sur la machine
    public bool tassePresent()
    {
        Vector2 posTasse = tasse.transform.position;
        Transform emplacementVerre = posVerre;
        if (Mathf.Sqrt(Mathf.Pow(posTasse.x - emplacementVerre.position.x, 2) + Mathf.Pow(posTasse.y - emplacementVerre.position.y, 2)) < 0.1f)
        {
            return true;
        }
        return false;
    }
}
