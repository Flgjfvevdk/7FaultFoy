using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ManageClient : MonoBehaviour
{
    public float tempsTotalJeu;
    private float tempsRestant;

    public float timeBtwClient;
    private float timeBtwNextClient;
    public List<Vector2> positionsFace;

    public GameObject client;
    public Transform spawn;


    // Start is called before the first frame update
    void Start()
    {
        tempsRestant = tempsTotalJeu;
        timeBtwNextClient = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempsRestant < 0)
        {
            //Debug.Log("Arretez tout !");
        } else
        {
            tempsRestant -= Time.deltaTime;
        }

        if(timeBtwNextClient <= 0)
        {
            if(positionsFace.Count > 0)
            {
                spawnClient();
                timeBtwNextClient = timeBtwClient;
            }
        } else
        {
            timeBtwNextClient -= Time.deltaTime;
        }
    }

    private void spawnClient()
    {
        Vector2 pos = positionsFace[0];
        positionsFace.RemoveAt(0);
        GameObject clientInstantie = Instantiate(client, spawn.position, Quaternion.identity);
        clientInstantie.GetComponent<clientDeplacement>().positionsVect.Add(new Vector2(pos.x, 7));
        clientInstantie.GetComponent<clientDeplacement>().positionsVect.Add(pos);
        clientInstantie.GetComponent<clientDemande>().mngClient = this;
        clientInstantie.GetComponent<clientDemande>().pointsScript = GetComponent<SC_Points>();

    }

    public void ajouter(Vector2 vect)
    {
        positionsFace.Add(vect);
    }



    
}
