using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject go;
    private SC_Points scr;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(this);
        scr = go.GetComponent<SC_Points>();
    }

    // Update is called once per frame
    void Update()
    {
        if (scr != null) {
            score = scr.point;
        }
    }
}
