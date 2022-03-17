using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_afficherFlou : MonoBehaviour
{
    public Image im;
    public void afficherFlou(bool b)
    {
        im.enabled = b;
    }
}
