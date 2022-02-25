using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class clientDemande : MonoBehaviour
{
    public static string[] demandes = new string[] {"Café", "Sirop", "Croissant"}; // a modifier apres 
    //public static string[] typeCafe = new string[] {"verre", "tasse "};
    //public static string[] typeBC = new string[] {"Snks", "Pluto", "Jueno", "katkit "};
    public static string[] typeSirop = new string[] {"Rouge", "Jaune", "Vert", "Noir", "Bleu", "Mauve "}; // a discuter 
    // public static String[] quantite = new String[] {"1", "2"};


    //public string maDemande;
    private int demandeIndex;

    private SpriteRenderer sprRend;
    public Sprite[] commandes;
    bool isTalking; 
    bool InRange;
    public int distanceJoueur = 3;
    public GameObject bulle;
    private GameObject bulleCree;
    private Transform tr;
    private Collider2D[] cols;

    public string maDemande;

    [System.NonSerialized]
    public SC_ManageClient mngClient;

    public float tempsMaxAttente;
    private float tempsAttente;

    public SC_Points pointsScript;

    private bool clientParti;
    
    void Start()
    {
        tempsAttente = tempsMaxAttente;
        clientParti = false;
        isTalking = false;
        InRange = false;
        tr = GetComponent<Transform>();
        // il a un seul fils 
        foreach (Transform child in transform) {
            if (child.gameObject.CompareTag("Commande")) {
                sprRend = child.gameObject.GetComponent<SpriteRenderer>();
            }
        }

        demandeIndex = Random.Range(0,demandes.Length);
        switch (demandeIndex) {
        case 0:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " " + typeCafe[Random.Range(0,typeCafe.Length)] + demandes[demandeIndex] + "SVP.";
            maDemande = demandes[0];
            sprRend.sprite = commandes[0];
            break;
        case 1:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] + typeSirop[Random.Range(0,typeSirop.Length)] + "SVP.";
            maDemande = demandes[1];
            sprRend.sprite = commandes[1];
            break;
        case 2:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] ;
            maDemande = demandes[2];
            sprRend.sprite = commandes[2];
            break;
        }

    }

    void Update()
    {
        InRange = false;
        // bulle apparait lorsque le client est suffisement proche du joueur 
        cols = Physics2D.OverlapCircleAll(tr.position, distanceJoueur);
        foreach (Collider2D elem in cols) {
            if (elem.gameObject.CompareTag("Player")) {
                InRange = true;
            }
        }

        if (InRange & !isTalking) {

            bulleCree = Instantiate(bulle, tr.position + new Vector3 (0,2,0),Quaternion.identity);
            bulleCree.GetComponent<Transform>().parent = tr;
            isTalking = true;
            //GetComponent<popUpTrigger>().showPopup();
            bulleCree.GetComponent<SC_Bulle>().popup = GetComponent<popUpTrigger>();


        }
        else if (!InRange & isTalking) {
            //Debug.Log("bulle destroyed client est loin");
            isTalking = false;
            if (bulleCree != null) {
                GetComponent<popUpTrigger>().hidePopup();
                Destroy(bulleCree);
            }
        }

        if(tempsAttente <= 0)
        {
            if (!clientParti)
            {
                demandeRate();
            }
        } else
        {
            tempsAttente -= Time.deltaTime;
        }
        
    }

    public void demandeSatisfaite()
    {
        clientParti = true;
        Debug.Log("yay ! J'ai reçu mon " + maDemande);
        if(mngClient != null)
        {
            mngClient.ajouter(transform.position);
        }

        pointsScript.addPoint(100.0f * tempsAttente/tempsMaxAttente);

        GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(transform.position.x, 20));
    }

    private void demandeRate()
    {
        Debug.Log("trop nul !");
        if (mngClient != null)
        {
            mngClient.ajouter(transform.position);
        }
        clientParti = true;

        GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(transform.position.x, 20));
        pointsScript.addPoint(-10);
    }
}
