using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class clientDemande : MonoBehaviour
{
    public static string[] demandes = new string[] {"Café", "Sirop", "Croissant", "Barre de chocolat"}; // a modifier apres 
    //public static string[] typeCafe = new string[] {"verre", "tasse "};
    //public static string[] typeBC = new string[] {"Snks", "Pluto", "Jueno", "katkit "};
    public static string[] typeSirop = new string[] {"Rouge", "Jaune", "Vert", "Noir", "Bleu", "Mauve "};
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


    
    void Start()
    {
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
            sprRend.sprite = commandes[0];
            break;
        case 1:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] + typeSirop[Random.Range(0,typeSirop.Length)] + "SVP.";
            sprRend.sprite = commandes[1];
            break;
        case 2:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] ;
            sprRend.sprite = commandes[2];
            break;
        case 3:
            //maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  +  typeBC[Random.Range(0,typeBC.Length)] + "SVP.";
            sprRend.sprite = commandes[3];
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
            
        }
        else if (!InRange & isTalking) {
            //Debug.Log("bulle destroyed client est loin");
            isTalking = false;
            if (bulleCree != null) {
                Destroy(bulleCree);
            }
        }
        
    }
}
