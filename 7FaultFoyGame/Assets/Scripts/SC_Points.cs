using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Points : MonoBehaviour
{
    // Start is called before the first frame update
    private int point;
    public Text textPoint;

    void Start()
    {
        point = 0;
        textPoint.text = "Points : 0";
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    addPoint(10);
        //}
    }

    public void addPoint(float value)
    {
        point += Mathf.RoundToInt(value);
        textPoint.text = "Points : " + point;
    }
}
