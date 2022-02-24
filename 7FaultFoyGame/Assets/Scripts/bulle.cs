using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulle : MonoBehaviour
{
    private Collider2D[] cols;
    SpriteRenderer spr;
    int distanceJoueur = 2;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        cols = Physics2D.OverlapCircleAll(transform.position, distanceJoueur);
        foreach (Collider2D elem in cols) {
            if (elem != null & elem.gameObject.CompareTag("Player")) {
                spr.enabled = true;
            }
            else {
                spr.enabled = false;
            }
        }
    }
}
