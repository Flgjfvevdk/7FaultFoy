using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_ManageClient : MonoBehaviour
{
    public float tempsTotalJeu;
    private float tempsRestant;

    public float timeBtwClient;
    private float timeBtwNextClient;
    public List<Vector2> positionsFace;

    public GameObject client;
    public Transform spawn;

    public Text textTimer;


    // Start is called before the first frame update
    void Start()
    {
        tempsRestant = tempsTotalJeu;
        timeBtwNextClient = 0;
        textTimer.text = "Temps restant : " + Mathf.RoundToInt(tempsRestant);
    }

    // Update is called once per frame
    void Update()
    {
        textTimer.text = "Temps restant : " + Mathf.RoundToInt(tempsRestant)/60 + "m"+(Mathf.RoundToInt(tempsRestant) - (Mathf.RoundToInt(tempsRestant) / 60) * 60);

        if (tempsRestant < 0)
        {
            SC_PointAfficherFinal.point = GetComponent<SC_Points>().point;
            SceneManager.LoadScene("Scene_Fin");
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
        clientDeplacement scClientDeplacement = clientInstantie.GetComponent<clientDeplacement>();
        scClientDeplacement.positionsVect.Add(new Vector2(pos.x, 7));
        scClientDeplacement.positionsVect.Add(pos);
        clientDemande scClientDemande = clientInstantie.GetComponent<clientDemande>();
        scClientDemande.mngClient = this;
        scClientDemande.pointsScript = GetComponent<SC_Points>();
        scClientDemande.enregistrement = pos;
    }

    public void ajouter(Vector2 vect)
    {
        positionsFace.Add(vect);
    }



    
}
