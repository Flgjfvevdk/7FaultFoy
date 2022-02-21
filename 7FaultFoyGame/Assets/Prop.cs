using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Prop : MonoBehaviour 
{
    public GameObject player;
    public GameObject popup;
    private Popup scr;
    private Transform tr;
    public float distance;

    public bool playerClose() {
        if (Mathf.Sqrt((tr.position.x-transform.position.x)*(tr.position.x-transform.position.x) + (tr.position.y-transform.position.y)*(tr.position.y-transform.position.y) ) < distance ) {
            return true;
            Debug.Log("YES");
        }
        return false;
    }

    public void showPopup() {
        scr.isVisible = true;
    }

    public void hidePopup() {
        scr.isVisible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        tr = player.GetComponent<Transform>();
        scr = popup.GetComponent<Popup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerClose()) {
            showPopup();
        } else {
            hidePopup();
        }
    }
}