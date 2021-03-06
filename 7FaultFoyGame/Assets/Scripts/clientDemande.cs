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
    
    public GameObject pasContent;
    public GameObject Content;
    [System.NonSerialized] public Vector2 enregistrement;

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
        if(demandeIndex == demandes.Length)
        {
            //Pour réduire les chances de tomber sur croissant, on relance si on tombe dessus
            demandeIndex = Random.Range(0, demandes.Length);
        }
        maDemande = demandes[demandeIndex];
        sprRend.sprite = commandes[demandeIndex];


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
        if(mngClient != null)
        {
            mngClient.ajouter(enregistrement);
        }

        if (bulleCree != null) {
            GetComponent<popUpTrigger>().hidePopup();
            Destroy(bulleCree);
        }

        GameObject obj = Instantiate(Content, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        obj.transform.parent = transform;

        GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, transform.position.y));
        GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, 20));
        if (mngClient.deuxJoueur && transform.position.x > 0)
        {
            pointsScript.addPointJ2(100.0f * tempsAttente / tempsMaxAttente);

        }
        else
        {
            pointsScript.addPoint(100.0f * tempsAttente / tempsMaxAttente);
        }
    }

    public void demandeRate()
    {
        clientParti = true;
        if (mngClient != null)
        {
            mngClient.ajouter(enregistrement);
            if (mngClient.deuxJoueur)
            {
                Vector2? vect = mngClient.demanderPosition(transform.position);
                if (vect.HasValue)
                {
                    GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, transform.position.y));
                    GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, vect.Value.y));
                    GetComponent<clientDeplacement>().positionsVect.Add(vect.Value);
                    enregistrement = vect.Value;
                    tempsAttente = tempsMaxAttente;
                    clientParti = false;

                }
            }
        }
        if (GetComponent<clientDeplacement>().positionsVect.Count == 0)
        {
            GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, transform.position.y));
            GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(0, 20));
            if(bulleCree != null)
            {
                GetComponent<popUpTrigger>().hidePopup();
                Destroy(bulleCree);
            }
        }
        

        GameObject obj = Instantiate(pasContent,transform.position + new Vector3(0,2,0), Quaternion.identity);
        obj.transform.parent = transform;

        if (mngClient.deuxJoueur && transform.position.x > 0) {
            pointsScript.addPointJ2(-10);

        } else {
            pointsScript.addPoint(-10);
        }
    }
}
