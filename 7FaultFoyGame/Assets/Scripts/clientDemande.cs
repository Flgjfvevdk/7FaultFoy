using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clientDemande : MonoBehaviour
{
    public static string[] demandes = new string[] {"Café ", "Sirop ", "Chocolatine ", "Croissant ", "Barre de chocolat"}; // a modifier apres 
    public static string[] typeCafe = new string[] {"verre ", "tasse "};
    public static string[] typeBC = new string[] {"Snks ", "Pluto ", "Fueno ", "katkit "};
    public static string[] typeSirop = new string[] {"Rouge ", "Jaune ", "Vert ", "Noir ", "Blanc ", "Mauve "};
    public static char[] quantite = new char[] {'1', '2'};
    public Text textObjet;

    public string maDemande;
    private int demandeIndex;


    // Start is called before the first frame update
    void Start()
    {
        demandeIndex = Random.Range(0,demandes.Length);
        switch (demandeIndex) {
        case 0:
            maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " " + typeCafe[Random.Range(0,typeCafe.Length)] + demandes[demandeIndex] + "SVP.";
            break;
        case 1:
            maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] + typeSirop[Random.Range(0,typeSirop.Length)] + "SVP.";
            break;
        case 2:
            maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] ;
            break;
        case 3:
            maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  + demandes[demandeIndex] ;
            break;
        case 4:
            maDemande = "je voudrais " + quantite[Random.Range(0,quantite.Length)] + " "  +  typeBC[Random.Range(0,typeBC.Length)] + "SVP.";
            break;
        }
        textObjet.text = maDemande;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
