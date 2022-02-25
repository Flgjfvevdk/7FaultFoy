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
    

    // Aide Test
    public bool clearTasse;

    public Sprite[] cafeFillingSprite;
    public Sprite[] siropFillingSprite;



    // Start is called before the first frame update
    void Start()
    {
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

        float maxFilling = Mathf.Max(fillingSirop, fillingRate);
        if (isCafe)
        {
            GetComponent<SpriteRenderer>().sprite = cafeFillingSprite[Mathf.RoundToInt(maxFilling * (cafeFillingSprite.Length - 1))];
        } else if (isSirop)
        {
            GetComponent<SpriteRenderer>().sprite = siropFillingSprite[Mathf.RoundToInt(maxFilling * (siropFillingSprite.Length - 1))];
        }
        
        
    }
}
