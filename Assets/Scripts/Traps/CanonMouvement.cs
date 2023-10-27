using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMouvement : MonoBehaviour
{
    
    [SerializeField] private int vitesse;
    [SerializeField] private float limiteHaute,limiteBasse;
    [SerializeField] private bool tir = false;
    [SerializeField] private bool canMove = true;
    public GameObject laser,charge;
    private int declancheur = 0;
    private bool attenteEnCours;


    private void Start()
    {
        attenteEnCours = false;
        if (this.transform.position.x < 0)
        {
            vitesse = -vitesse;
        }
    }
    void Update()
    {


        if (tir)
        {
            if (canMove)
            {
                canMove = false;
                charge.SetActive(true);
                StartCoroutine(AttenteCoroutine(2f));
            }
            
            


        }
        else
        {

            if (transform.position.y > limiteHaute && vitesse>0)
            {

                vitesse = -vitesse;
            }
            else if (transform.position.y < limiteBasse && vitesse<0)
            {

                vitesse = -vitesse;
            }
            transform.Translate(Vector3.up * Time.deltaTime*vitesse);
            

            declancheur++;
            if (declancheur > 3600)
            {
                Debug.Log("actif");
                tir = true;
            }

        }

    }

    IEnumerator AttenteCoroutine(float sec)
    {
        attenteEnCours = true;

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (sec == 2f)
        {
            charge.SetActive(false);
            laser.SetActive(true);
            StartCoroutine(AttenteCoroutine(3f));
        }
        else
        {
            laser.SetActive(false);
            declancheur = 0;
            tir = false;
            canMove = true;
            
            attenteEnCours = false;
        }

       
    }

}
