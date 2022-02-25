using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Viennoiserie : MonoBehaviour
{
    [Range(0f, 1f)] public float cookingRate;

    public Sprite spriteChaud;
    private bool spriteChanged;
    public bool isCook;

    // Start is called before the first frame update
    void Start()
    {
        isCook = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cookingRate >= 1 && !spriteChanged)
        {
            GetComponent<SpriteRenderer>().sprite = spriteChaud;
            spriteChanged = true;
        }
    }
}
