using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TextMenu : MonoBehaviour
{
    // Savoir quel type de Text on a
    public bool isJouer;
    public bool isOption;
    public bool isCredits;

    public float timeOnText;

    // Temps qu'il faut attendre sur le text avant le changement de scène
    public float timeBeforeChange;

    //Savoir si le joueur est sur le text
    private Collider2D[] col;

    public GameObject player;



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
                //Charger la scène de jeu
            }
            else if (isOption)
            {
                //Charger la scène des options
            }
            else if (isCredits)
            {
                //Charger la scène des credits
            }
        }
        else
        {
            // TODO matcher la taille de la OverlapBox avec la taille du text, 
            // regarder si le joueur est dedant (faire une barre de chargement corespondant à timeOnText) ajouter les changement de scène
            //col = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0f);
            //Debug.Log(transform.position);
            col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1f);
            foreach (Collider2D elem in col)
            {
                if (elem.gameObject.CompareTag("Player"))
                {
                    Debug.Log("I'am in");
                    timeOnText += Time.deltaTime;
                }
                else
                {
                    //timeOnText = 0f;
                }
            }


        }
    }
}
