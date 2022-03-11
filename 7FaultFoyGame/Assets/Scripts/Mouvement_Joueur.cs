using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement_Joueur : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = Mathf.Sqrt(2) / 2.0f;

    public float runSpeed;

    private Collider2D[] col;
    private Transform tr;
    public bool IsLifting;
    private GameObject ObjectLifted;
    private char direction;

    public GameObject verreGO;
    public GameObject croissantGO;
    public Transform centrePlayer;
    private int[] dir;

    public int numeroJoueur; // 0 = il n'y a pas de j2 // 1 = j1 // 2 = J2
    private KeyCode keyAction;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        IsLifting = false;

        dir = new int[2] { 0, -1 };

        if(numeroJoueur == 1)
        {
            keyAction = KeyCode.R;
        } else if (numeroJoueur == 2)
        {
            keyAction = KeyCode.I;
        } else
        {
            keyAction = KeyCode.Space;
        }

    }

    void Update()
    {
        if(ObjectLifted == null && IsLifting)
        {
            IsLifting = false;
        }


        // Gives a value between -1 and 1
        if(numeroJoueur == 1)
        {
            if (Input.GetKey(KeyCode.D)) {
                horizontal = 1;
            } else if (Input.GetKey(KeyCode.Q)) {
                horizontal = -1;
            } else{
                horizontal = 0;
            }
            if (Input.GetKey(KeyCode.Z)) {
                vertical = 1;
            }
            else if (Input.GetKey(KeyCode.S)) {
                vertical = -1;
            } else {
                vertical = 0;
            }
        } else if(numeroJoueur == 2) {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                horizontal = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 0;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                vertical = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                vertical = -1;
            }
            else
            {
                vertical = 0;
            }
        } else { 
            horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        }

        if (Input.GetKeyDown(keyAction) & !IsLifting)
        {
            col = Physics2D.OverlapCircleAll(new Vector2(centrePlayer.position.x, centrePlayer.position.y), 1.2f);


            GameObject objectTarget = null;
            float distanceObjTarget = Mathf.Infinity;

            foreach (Collider2D elem in col)
            {
                GameObject elemGO = elem.gameObject;
                if ((elemGO.CompareTag("evier") || elemGO.CompareTag("boiteCroissant") || elemGO.CompareTag("attrapable")) && (Vector2.Distance(transform.position, elem.transform.position) < distanceObjTarget))
                {
                    objectTarget = elem.gameObject;
                    distanceObjTarget = Vector2.Distance(transform.position, elem.transform.position);
                }

            }
            if (objectTarget != null)
            {
                if (objectTarget.CompareTag("evier"))
                {

                    GameObject verreReel = Instantiate(verreGO, transform.position, Quaternion.identity);
                    ObjectLifted = verreReel;
                    tr = verreReel.GetComponent<Transform>();
                    tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    tr.SetParent(transform, true);
                    IsLifting = true;
                }

                if (objectTarget.CompareTag("boiteCroissant"))
                {
                    GameObject croissantReel = Instantiate(croissantGO, transform.position, Quaternion.identity);
                    ObjectLifted = croissantReel;
                    tr = croissantReel.GetComponent<Transform>();
                    tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    tr.SetParent(transform, true);
                    IsLifting = true;
                }

                if (objectTarget.CompareTag("attrapable"))
                {
                    ObjectLifted = objectTarget;
                    tr = objectTarget.GetComponent<Transform>();
                    tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    tr.SetParent(transform, true);
                    ObjectLifted.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    IsLifting = true;
                }
            }
        }
        else if (Input.GetKeyDown(keyAction) & IsLifting)
        {
            //On recup tous les trucs à côté.
            col = Physics2D.OverlapCircleAll(new Vector2(centrePlayer.position.x, centrePlayer.position.y), 1f);

            bool donnePnj = false;
            bool actionFaite = false;

            //On regarde s'il y a un pnj qui a demandé l'objet porté à proximité
            foreach (Collider2D elem in col)
            {
                if (elem.gameObject.CompareTag("client") && ObjectLifted != null)
                {
                    string objetTenueStr = "";
                    if(ObjectLifted.GetComponent<SC_Viennoiserie>() != null && ObjectLifted.GetComponent<SC_Viennoiserie>().isCook)
                    {
                        objetTenueStr = "Croissant";
                    } else if (ObjectLifted.GetComponent<SC_Tasse>() != null)
                    {
                        SC_Tasse scTasse = ObjectLifted.GetComponent<SC_Tasse>();
                        if(scTasse.isSirop && scTasse.fillingSirop >= 1.0f)
                        {
                            objetTenueStr = "Sirop";
                        } else if(scTasse.isCafe && scTasse.fillingRate >= 1.0f)
                        {
                            objetTenueStr = "Café";
                        }
                    }

                    if(objetTenueStr != "")
                    {
                        clientDemande scClientDemande = elem.GetComponent<clientDemande>();
                        if (scClientDemande != null && scClientDemande.maDemande == objetTenueStr) 
                        {
                            if(objetTenueStr == "Croissant")
                            {
                                SC_Viennoiserie scVienoiserie = ObjectLifted.GetComponent<SC_Viennoiserie>();
                                if(scVienoiserie != null &&  scVienoiserie.isCook)
                                {
                                    scClientDemande.demandeSatisfaite();
                                }
                                else
                                {
                                    scClientDemande.demandeRate();
                                }
                            }
                            else{
                                scClientDemande.demandeSatisfaite();
                            }
                            Destroy(ObjectLifted);
                            IsLifting = false;
                            ObjectLifted = null;
                            donnePnj = true;
                        }

                    }
                }
            }
            
            if (!donnePnj)
            {
                
                //On regarde si il y a une machine proche qui peut intéragir avec l'objet porté
                foreach (Collider2D elem in col)
                {
                    if (!actionFaite && elem.gameObject.CompareTag("cafetiere") && ObjectLifted.GetComponent<SC_Tasse>() != null)
                    {
                        SC_cafetiere scCafetiere = elem.GetComponent<SC_cafetiere>();
                        if (scCafetiere.isMachineReady) // Il serait bien de vérifier que l'objet est bien une tasse && ObjectLifted.CompareTag("tasse")
                        {
                            scCafetiere.makeCoffee(ObjectLifted);
                            tr.parent = null;
                            IsLifting = false;
                        } else if (scCafetiere.isCoffeeReady && scCafetiere.tassePresent())
                        {
                            GameObject nouveauTasse = scCafetiere.tasse;
                            scCafetiere.makeCoffee(ObjectLifted);
                            ObjectLifted.transform.parent = null;

                            ObjectLifted = nouveauTasse;
                            tr = ObjectLifted.GetComponent<Transform>();
                            tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                            tr.SetParent(transform, true);
                            ObjectLifted.GetComponent<SpriteRenderer>().sortingOrder = 10;
                            actionFaite = true;
                        }
                    }
                    else if (!actionFaite && elem.gameObject.CompareTag("distributeurSirop") && ObjectLifted.GetComponent<SC_Tasse>() != null)
                    {
                        SC_DistributeurSirop scDistriSirop = elem.GetComponent<SC_DistributeurSirop>();
                        if (scDistriSirop.isDistributeurReady) // Il serait bien de vérifier que l'objet est bien une tasse && ObjectLifted.CompareTag("tasse")
                        {
                            scDistriSirop.makeSirop(ObjectLifted);
                            tr.parent = null;
                            IsLifting = false;
                        }
                        else if (scDistriSirop.isSiropReady && scDistriSirop.tassePresent()) 
                        {
                            GameObject nouveauVerre = scDistriSirop.tasse;
                            scDistriSirop.makeSirop(ObjectLifted);
                            ObjectLifted.transform.parent = null;

                            ObjectLifted = nouveauVerre; 
                            tr = ObjectLifted.GetComponent<Transform>();
                            tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                            tr.SetParent(transform, true);
                            ObjectLifted.GetComponent<SpriteRenderer>().sortingOrder = 10;
                            actionFaite = true;
                        }
                    }
                    else if (!actionFaite && elem.gameObject.CompareTag("four") && ObjectLifted.GetComponent<SC_Viennoiserie>() != null)
                    {
                        SC_Four scFour = elem.GetComponent<SC_Four>();
                        if (scFour.isFourReady) // Il serait bien de vérifier que l'objet est bien une tasse && ObjectLifted.CompareTag("tasse")
                        {
                            scFour.cookViennoiserie(ObjectLifted);
                            tr.parent = null;
                            IsLifting = false;
                        } else if (scFour.isViennoiserieReady && scFour.viennoiseriePresent())
                        {
                            GameObject nouveauCroissant = scFour.viennoiserie;
                            scFour.cookViennoiserie(ObjectLifted);
                            ObjectLifted.transform.parent = null;

                            ObjectLifted = nouveauCroissant;
                            tr = ObjectLifted.GetComponent<Transform>();
                            tr.SetPositionAndRotation(transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                            tr.SetParent(transform, true);
                            ObjectLifted.GetComponent<SpriteRenderer>().sortingOrder = 10;
                            actionFaite = true;
                        }
                    }
                }

            }

            //On n'a pas trouver de cafetiere ou autre
            if (IsLifting && !actionFaite)
            {
                tr = ObjectLifted.GetComponent<Transform>();

                //On pose le verre vers la ou regarde le pj
                tr.position = centrePlayer.position + new Vector3(1f * dir[0], 1.2f*dir[1], 0);
                tr.parent = null;
                ObjectLifted.GetComponent<SpriteRenderer>().sortingOrder = 6;
                IsLifting = false;
            }


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
        if (vertical > 0)
        {
            anim.SetBool("GoBack", true);
            anim.SetBool("GoForward", false);
            dir[0] = 0;
            dir[1] = 1;
        } else if(vertical < 0)
        {
            anim.SetBool("GoBack", false);
            anim.SetBool("GoForward", true);
            dir[0] = 0;
            dir[1] = -1;
        }
        else
        {
            anim.SetBool("GoBack", false);
            anim.SetBool("GoForward", false);
        }

        if (horizontal > 0)
        {
            anim.SetBool("GoRight", true);
            anim.SetBool("GoLeft", false);
            dir[0] = 1;
            dir[1] = 0;
        }
        else if (horizontal < 0)
        {
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", true);
            dir[0] = -1;
            dir[1] = 0;
        } else
        {
            anim.SetBool("GoRight", false);
            anim.SetBool("GoLeft", false);
        }

    }
}
