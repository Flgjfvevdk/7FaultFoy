using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tasse : MonoBehaviour
{
    // Café
    [Range(0f, 1f)] public float fillingRate;
    public bool isCafe;
    
    // Sirop
    [Range(0f, 1f)] public float fillingSirop;
    public bool isSirop;
    

    // Autre
    public bool onCafetiere;

    // Aide Test
    public bool clearTasse;



    // Start is called before the first frame update
    void Start()
    {
        onCafetiere = false;

        clearTasse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(clearTasse){
            isSirop = false;
            isCafe = false;
            fillingRate = 0f;
            fillingSirop = 0f;
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
