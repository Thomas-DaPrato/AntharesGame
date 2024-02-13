using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    [SerializeField] private int tempsCharge;
    [SerializeField] private int declancheur=0;
    [SerializeField]
    AudioSource son;
    public AudioClip charge,sonTir;
    public GameObject erruption,vib;
    private bool tremble,tir,intermediaire, intermediaire2 = false, changeRound=false;


    private void Start()
    {
        StartCoroutine(Attentepiege(tempsCharge));
    }
    void Update()
    {
        if (!changeRound) { 
            if (tremble)
            {
                intermediaire2 = true;

                //tremblement
                vib.SetActive(true);
                son.PlayOneShot(charge);
                StartCoroutine(AttenteCoroutine(5f));
                tremble = false;
            
            }
            else if (tir)
            {
                intermediaire = true;
                StartCoroutine(AttenteCoroutine(5f));
                erruption.SetActive(true);
                tir = false;
            
            }

        
            if (declancheur > tempsCharge)
            {
                declancheur = 0;
                //Debug.Log("actif");
                tremble = true;
            }

        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
        {
            declancheur = 0;
            if (intermediaire2)
            {
                //fin du tremblement
                son.PlayOneShot(sonTir);
                
                tir = true;
                intermediaire2 = false;
            }


            if (intermediaire)
            {

                erruption.SetActive(false);
                vib.SetActive(false);
                intermediaire = false;
                StartCoroutine(Attentepiege(tempsCharge));
            }

        }

    }


    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
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

    public void ChangeRound(float tempRound)
    {
        changeRound = true;
        vib.SetActive(false);
        erruption.SetActive(false);
        intermediaire2 = false;
        changeRound = false;
        tir = false;
        StartCoroutine(AttenteRound(tempRound));
    }



}
