using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clientDeplacement : MonoBehaviour
{

    public Stack<Vector2> positions = new Stack<Vector2>();
    public List<Vector2> positionsVect;

    public float speed;

    private Transform tr;
    private Rigidbody2D rb;

    private const float minimalDistance = 0.2f;
    



    // Start is called before the first frame update
    void Start()
    {   
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(positionsVect.Count > 0)
        {
            //positions.Push(positionsVect[0]);
            if (!Near(positionsVect[0]))
            {
                MoveTowards(positionsVect[0]);
            }
            else
            {
                positionsVect.RemoveAt(0);
                rb.velocity = new Vector2(0, 0);
            }
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }

    }


    private void MoveTowards(Vector2 vect) {
        if (Mathf.Abs(tr.position.x - vect.x) > minimalDistance) {
            if (tr.position.x > vect.x) {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            } else if (tr.position.x < vect.x) {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        } else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Mathf.Abs(tr.position.y - vect.y) > minimalDistance) {
            if (tr.position.y > vect.y) {
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            } else if (tr.position.y < vect.y) {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
        } else {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    private bool Near(Vector2 vect) {
        if (Mathf.Abs(tr.position.x - vect.x) < minimalDistance & Mathf.Abs(tr.position.y - vect.y) < minimalDistance) {
            return true;
        }
        return false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float rdAngle = Random.Range(0, 2 * Mathf.PI);
        rb.velocity = new Vector2(Mathf.Cos(rdAngle), Mathf.Sin(rdAngle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float rdAngle = Random.Range(0, 2 * Mathf.PI);
        rb.velocity = new Vector2(Mathf.Cos(rdAngle), Mathf.Sin(rdAngle));
    }
}
