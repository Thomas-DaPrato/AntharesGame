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
    public bool flammeBouge = false;
    public float dureeCharge = 2;
    



    private void Start()
    {
        
        StartCoroutine(Attentepiege(tempsCharge));
    }
    void Update()
    {
        if (!changeRound) { 
            if (canMove) {

                if (flammeBouge)
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
                    // chargement des flammes
                    chargePiege(true);
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

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if (!changeRound) { 
            if (sec== dureeCharge)
            {
                //arret de la vibration
                activeFlammes();
            
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

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if(!changeRound)
        declancheur = tempsCharge + 1;


    }
    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        changeRound = false;
        StartCoroutine(Attentepiege(tempsCharge));
        



    }

    public void ChangeRound(float tempRound)
    {
        changeRound = true;
        declancheur = 0;
        canMove = false;
        attente = false;
        flammes.SetActive(false);
        StartCoroutine(AttenteRound(tempRound));
    }

    private void chargePiege(bool chargeOuPas)
    {
        if (chargeOuPas)
        {
            vib.SetActive(true);
            son.PlayOneShot(charge);
            StartCoroutine(AttenteCoroutine(dureeCharge));
            attente = true;
        }
        else
        {
            vib.SetActive(false);
        }
        
    }
    private void activeFlammes()
    {
        chargePiege(false);
        //son.PlayOneShot(sonTir);
        flammes.SetActive(true);
    }


}


