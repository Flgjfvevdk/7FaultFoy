using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_Joueur : MonoBehaviour
{
   Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = Mathf.Sqrt(2)/2.0f;

    public float runSpeed;

    private Collider2D[] col;
    private Transform tr; 
    public bool IsLifting;
    private GameObject ObjectLifted;
    private char direction; 

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        IsLifting = false;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (Input.GetKeyDown(KeyCode.Space) & !IsLifting)
        {
            col = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1.0f);
            foreach (Collider2D elem in col) {
                if (elem.gameObject.CompareTag("attrapable")) {
                    ObjectLifted = elem.gameObject;
                    //Debug.Log(elem.name);
                    tr = elem.gameObject.GetComponent<Transform>();
                    tr.SetPositionAndRotation(transform.position + new Vector3 (0,1,0), Quaternion.identity);
                    tr.SetParent(transform,true);
                    IsLifting = true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) & IsLifting) {

                tr = ObjectLifted.GetComponent<Transform>();
                tr.localPosition = new Vector3 (1, -0.2f, 0);
                tr.parent = null; 
                IsLifting = false;
            
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);


        //On modifie le sprite du joueur selon sa direction
        Animator anim = GetComponent<Animator>();
        if (horizontal > 0)
        {
            anim.SetBool("GoRight", true);
            anim.SetBool("GoLeft", false);
        } else if(horizontal < 0)
        {
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", true);
        } else
        {
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", false);
        }

        if(vertical > 0)
        {
            anim.SetBool("GoBack", true);
        } else
        {
            anim.SetBool("GoBack", false);
        }
    }
}
