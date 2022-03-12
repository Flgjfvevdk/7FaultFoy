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
    private List<Vector2> positionsFace2;

    public GameObject client;
    public GameObject client2J;
    public Transform spawn;
    public Transform spawn2;

    public Text textTimer;

    public bool deuxJoueur;

    // Start is called before the first frame update
    void Start()
    {
        tempsRestant = tempsTotalJeu;
        timeBtwNextClient = 0;
        textTimer.text = "Temps restant : " + Mathf.RoundToInt(tempsRestant);

        positionsFace2 = new List<Vector2>(positionsFace);
    }

    // Update is called once per frame
    void Update()
    {
        textTimer.text = "Temps restant : " + Mathf.RoundToInt(tempsRestant)/60 + "m"+(Mathf.RoundToInt(tempsRestant) - (Mathf.RoundToInt(tempsRestant) / 60) * 60);

        if (tempsRestant < 0)
        {
            if(deuxJoueur){
                SC_PointAfficherFinal2J.point1 = GetComponent<SC_Points>().point;
                SC_PointAfficherFinal2J.point2 = GetComponent<SC_Points>().pointJ2;
                SceneManager.LoadScene("Scene_Fin 2J");
            } else {
                SC_PointAfficherFinal.point = GetComponent<SC_Points>().point;
            
                SceneManager.LoadScene("Scene_Fin");
            }
            
        } else
        {
            tempsRestant -= Time.deltaTime;

        }

        if(timeBtwNextClient <= 0)
        {
            if (!deuxJoueur)
            {
                if(positionsFace.Count > 0)
                {
                    spawnClient();
                    timeBtwNextClient = timeBtwClient;
                }
            } else
            {
                spawn2Client();
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

    private void spawn2Client()
    {
        //On fait spawn le premier client
        if (positionsFace.Count > 0)
        {
            int rd = Random.Range(0, positionsFace.Count);
            Vector2 pos = positionsFace[rd];
            positionsFace.RemoveAt(rd);
            GameObject clientInstantie = Instantiate(client2J, spawn.position, Quaternion.identity);
            clientDeplacement scClientDeplacement = clientInstantie.GetComponent<clientDeplacement>();
            scClientDeplacement.positionsVect.Add(new Vector2(spawn.position.x, pos.y));
            scClientDeplacement.positionsVect.Add(new Vector2(- Mathf.Abs(pos.x), pos.y));
            clientDemande scClientDemande = clientInstantie.GetComponent<clientDemande>();
            scClientDemande.mngClient = this;
            scClientDemande.pointsScript = GetComponent<SC_Points>();
            scClientDemande.enregistrement = new Vector2(-Mathf.Abs(pos.x), pos.y);
        }

        //On fait spawn le deuxi�me client
        if(positionsFace2.Count > 0)
        {
            int rd = Random.Range(0, positionsFace2.Count);
            Vector2 pos2 = positionsFace2[rd];
            positionsFace2.RemoveAt(rd);
            GameObject clientInstantie2 = Instantiate(client2J, spawn2.position, Quaternion.identity);
            clientDeplacement scClientDeplacement2 = clientInstantie2.GetComponent<clientDeplacement>();
            scClientDeplacement2.positionsVect.Add(new Vector2(spawn2.position.x, pos2.y));
            scClientDeplacement2.positionsVect.Add(new Vector2(Mathf.Abs(pos2.x), pos2.y));
            clientDemande scClientDemande2 = clientInstantie2.GetComponent<clientDemande>();
            scClientDemande2.mngClient = this;
            scClientDemande2.pointsScript = GetComponent<SC_Points>();
            scClientDemande2.enregistrement = new Vector2(Mathf.Abs(pos2.x), pos2.y);
        }
    }

    public void ajouter(Vector2 vect)
    {
        if (!deuxJoueur)
        {
            positionsFace.Add(vect);
        } else
        {
            if (vect.x < 0)
            {
                positionsFace.Add(vect);
            } else
            {
                positionsFace2.Add(vect);
            }
        }
    }

    public Vector2? demanderPosition(Vector2 ancienPos)
    {
        Vector2? vect = null;
        if(ancienPos.x < 0)
        {
            //Le client �tait � droite, on va voir si on peut le faire aller � gauche
            if(positionsFace2.Count > 0)
            {
                int rd = Random.Range(0, positionsFace2.Count);
                Vector2 pos2 = positionsFace2[rd];
                positionsFace2.RemoveAt(rd);
                vect = pos2;
            }
        } else
        {
            //Le client �tait � gauche, on va voir si on peut le faire aller � droite
            if (positionsFace.Count > 0)
            {
                int rd = Random.Range(0, positionsFace.Count);
                Vector2 pos = positionsFace[rd];
                positionsFace.RemoveAt(rd);
                vect = pos;
            }
        }


        return vect;
    }




}
