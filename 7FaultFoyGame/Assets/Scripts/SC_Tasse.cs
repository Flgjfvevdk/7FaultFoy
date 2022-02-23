using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Tasse : MonoBehaviour
{
    [Range(0f, 1f)] public float fillingRate;
    public bool isCafe;
    public bool isSirop;
    public bool onCafetiere;



    // Start is called before the first frame update
    void Start()
    {
        onCafetiere = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
