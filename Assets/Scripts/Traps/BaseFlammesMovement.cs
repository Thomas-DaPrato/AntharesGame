using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFlammesMovement : MonoBehaviour
{

    [SerializeField] private int vitesse, tempsCharge;
    [SerializeField] private float limiteHaute, limiteBasse;
    [SerializeField]
    AudioSource son;
    public GameObject flammes,vib;
    public AudioClip charge/*,sonTir*/;
    private bool canMove = true;
    private bool attente = false;
    private int declancheur = 0;
    public bool mouvementPermis = false;

    void Update()
    {
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

        declancheur++;
        if (declancheur > tempsCharge)
        {
            declancheur = 0;
            Debug.Log("actif");
            canMove = false;
        }
    }


    IEnumerator AttenteCoroutine(float sec)
    {
        
        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
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
            declancheur = 0;
            canMove = true;
            attente = false;
            flammes.SetActive(false);
        }



        }


    }


