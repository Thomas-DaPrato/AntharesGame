using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFlammesMovement : MonoBehaviour
{

    [SerializeField] private int vitesse, tempsCharge;
    [SerializeField] private float limiteHaute, limiteBasse;
    [SerializeField]
    AudioSource son;

    [HideInInspector] public bool changeRound = false;


    public GameObject flammes,vib;
    public AudioClip charge/*,sonTir*/;
    private bool canMove = true;
    private bool attente = false;
    private int declancheur = 0;
    public bool mouvementPermis = false;
    



    private void Start()
    {
        
        StartCoroutine(Attentepiege(tempsCharge));
    }
    void Update()
    {
        if (!changeRound) { 
            if (canMove) {

                if (mouvementPermis)
                {
                    if (transform.position.x < limiteBasse && vitesse > 0)
                    {

                        vitesse = -vitesse;
                    }
                    else if (transform.position.x > limiteHaute && vitesse < 0)
                    {

                        vitesse = -vitesse;
                    }
                    transform.Translate(Vector3.left * Time.deltaTime * vitesse);
                }
            
            }
            else
            {
                if (attente == false)
                {
                    // vibration de la plateform
                    vib.SetActive(true);
                    son.PlayOneShot(charge);
                    StartCoroutine(AttenteCoroutine(2f));
                    attente = true;
                }
            


            }


            if (declancheur > tempsCharge)
            {
                declancheur = 0;
                canMove = false;
            }

        }
    }


    IEnumerator AttenteCoroutine(float sec)
    {
        
        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (!changeRound) { 
            if (sec==2)
            {
                //arret de la vibration
                vib.SetActive(false);
                //son.PlayOneShot(sonTir);
                flammes.SetActive(true);
            
                StartCoroutine(AttenteCoroutine(5f));
            }
            else
            {
                StartCoroutine(Attentepiege(tempsCharge));
                canMove = true;
                attente = false;
                flammes.SetActive(false);
            }


        }
    }

    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if(!changeRound)
        declancheur = tempsCharge + 1;


    }
    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        changeRound = false;
        StartCoroutine(Attentepiege(tempsCharge));
        



    }

    public void ChangeRound()
    {
        changeRound = true;
        canMove = true;
        attente = false;
        flammes.SetActive(false);
        StartCoroutine(AttenteRound(7f));
    }


}


