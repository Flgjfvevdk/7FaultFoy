using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Bulle : MonoBehaviour
{
    [System.NonSerialized]
    public popUpTrigger popup;
    public bool mustPrintPopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(popup != null && mustPrintPopup)
        {
            popup.showPopup();
        }
    }
}
