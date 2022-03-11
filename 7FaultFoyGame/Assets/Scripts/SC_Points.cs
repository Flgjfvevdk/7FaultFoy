using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Points : MonoBehaviour
{
    // Start is called before the first frame update
    public int point;
    public int pointJ2;
    public Text textPoint;
    public Text textPointJ2;

    void Start()
    {
        point = 0;
        pointJ2 = 0;
        textPoint.text = "Points : 0";
        if(textPointJ2 != null)
        {
            textPointJ2.text = "Points : 0";
        }
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

    public void addPointJ2(float value)
    {
        pointJ2 += Mathf.RoundToInt(value);
        textPointJ2.text = "Points : " + pointJ2;
    }
}
