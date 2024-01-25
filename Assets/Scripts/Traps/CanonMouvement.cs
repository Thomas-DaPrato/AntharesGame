using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMouvement : MonoBehaviour
{
    
    [SerializeField] private int vitesse, tempsCharge;
    [SerializeField] private Transform limiteHaute,limiteBasse;
    [SerializeField] private bool tir = false;
    [SerializeField] private bool canMove = true;
    [SerializeField]
    AudioSource son;
    public AudioClip chargement,sonTir;
    public GameObject laser,charge;
    private int declancheur = 0;
    private bool attenteEnCours = false;
    private bool changeRound = false;


    private void Start()
    {
        StartCoroutine(Attentepiege(tempsCharge));
        attenteEnCours = false;

        
        if (this.transform.position.x < 0)
        {
            vitesse = -vitesse;
        }
    }
    void Update()
    {

        if (!changeRound) { 
            if (tir)
            {
                if (canMove)
                {
                    canMove = false;
                    charge.SetActive(true);
                    son.PlayOneShot(chargement);
                    StartCoroutine(AttenteCoroutine(2.3f));
                }




            }
            else
            {
                if (transform.position.y > limiteHaute.position.y)
                {
                    vitesse = -vitesse;
                }
                else if (transform.position.y < limiteBasse.position.y)
                {
                    vitesse = -vitesse;
                }
                transform.Translate(Vector3.forward * Time.deltaTime * vitesse);


                
                if (declancheur > tempsCharge)
                {
                    declancheur = 0;
                    tir = true;
                }

            }
        }

    }

    public void stopSound()
    {
        son.Stop();
    }

    IEnumerator AttenteCoroutine(float sec)
    {
        if (!changeRound) { 
            attenteEnCours = true;

            // Attendez pendant x secondes
            yield return new WaitForSeconds(sec);

            // Après l'attente, vous pouvez mettre votre code ici
            if (sec == 2.3f && !changeRound)
            {
                charge.SetActive(false);
                laser.SetActive(true);
                son.PlayOneShot(sonTir);
                StartCoroutine(AttenteCoroutine(3f));
            }
            else if (!changeRound)
            {
                laser.SetActive(false);
                declancheur = 0;
                tir = false;
                canMove = true;
                StartCoroutine(Attentepiege(tempsCharge));

                attenteEnCours = false;
            }

        }
    }


    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        changeRound = false;        
        declancheur = 0;
        canMove = true;
        attenteEnCours = false;



    }

    public void ChangeRound(float tempRound)
    {
        changeRound = true;
        declancheur = 0;
        tir = false;
        laser.SetActive(false);
        StartCoroutine(AttenteRound(tempRound));
    }
    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
            declancheur = tempsCharge + 1;



    }

}
