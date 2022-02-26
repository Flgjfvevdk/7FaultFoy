using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_PointAfficherFinal : MonoBehaviour
{

    public static int point;
    public Text textPoint;
    // Start is called before the first frame update
    void Start()
    {
        textPoint.text = "Points : " + point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
