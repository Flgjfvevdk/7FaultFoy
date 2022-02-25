using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreFinale : MonoBehaviour
{
    private GameObject go;
    private Score sc;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.FindWithTag("Score");
        sc = go.GetComponent<Score>();
        GetComponent<Text>().text = "Points : " + sc.score.ToString();
    }
}
