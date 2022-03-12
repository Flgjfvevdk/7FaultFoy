using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_PointAfficherFinal2J : MonoBehaviour
{

    public static int point1;
    public static int point2;
    public Text textPoint1;
    public Text textPoint2;
    public Text messageDeFin;
    // Start is called before the first frame update
    void Start()
    {
        textPoint1.text = "Points J1 : " + point1;
        textPoint2.text = "Points J2 : " + point2;
        if(point1 > point2){
            messageDeFin.text = "Le joueur 1 a gagne !";
        } else if( point1 < point2){
            messageDeFin.text = "Le joueur 2 a gagne !";
        } else {
            messageDeFin.text = "Egalite parfaite !";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
