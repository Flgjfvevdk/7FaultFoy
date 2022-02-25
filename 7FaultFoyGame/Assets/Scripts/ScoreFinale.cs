using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFinale : MonoBehaviour
{
    private GameObject go;
    private Score sc;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.FindTag("Score");
        go.text = "Points : " + sc.score.ToString();
    }

}
