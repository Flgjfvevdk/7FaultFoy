using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ClientApparence : MonoBehaviour
{
    public Sprite[] spriteCorps;
    public Sprite[] spritePossible_Tshirt;
    public Sprite[] spritePossible_cheveux;
    public Sprite[] spritePossible_chaussure;

    public SpriteRenderer tshirt;
    public SpriteRenderer cheveux;
    public SpriteRenderer chaussure;

    public Rigidbody2D rb;

    private int indexTshirt;
    private int indexCheveux;
    private int indexChaussure;
    private int orientationIndex;

    // Start is called before the first frame update
    void Start()
    {
        orientationIndex = 0;
        int max = spritePossible_Tshirt.Length;
        indexTshirt = Random.Range(0, max/4);

        indexCheveux = Random.Range(0, spritePossible_cheveux.Length/4);

        indexChaussure = Random.Range(0, spritePossible_chaussure.Length/4);

    }

    // Update is called once per frame
    void Update()
    {
        tshirt.sprite = spritePossible_Tshirt[indexTshirt * 4 + orientationIndex];
        cheveux.sprite = spritePossible_cheveux[indexCheveux * 4 + orientationIndex];
        chaussure.sprite = spritePossible_chaussure[indexChaussure * 4 + orientationIndex];
        GetComponent<SpriteRenderer>().sprite = spriteCorps[orientationIndex];

        if(rb.velocity.y < 0)
        {
            orientationIndex = 0;
        } else if(rb.velocity.y > 0)
        {
            orientationIndex = 2;
        } else
        {
            if(rb.velocity.x > 0)
            {
                orientationIndex = 1;
            } else if(rb.velocity.x < 0)
            {
                orientationIndex = 3;
            } else
            {
                orientationIndex = 0;
            }
        }

    }
}
