using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserBehaviour : MonoBehaviour
{
    [SerializeField] private int tempsCharge;
    [SerializeField] private int declancheur=0;
    [SerializeField]
    AudioSource son;
    public AudioClip charge;
    public GameObject erruption;
    private bool tremble,tir,intermediaire, intermediaire2 = false;
    void Update()
    {
        if (tremble)
        {
            intermediaire2 = true;

            //tremblement
            son.PlayOneShot(charge);
            StartCoroutine(AttenteCoroutine(2f));
            tremble = false;
            
        }
        else if (tir)
        {
            intermediaire = true;
            StartCoroutine(AttenteCoroutine(1f));
            erruption.SetActive(true);
            tir = false;
            
        }

        declancheur++;
        if (declancheur > tempsCharge)
        {
            declancheur = 0;
            Debug.Log("actif");
            tremble = true;
        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        declancheur = 0;
        if (intermediaire2)
        {
            //fin du tremblement
            tir = true;
            intermediaire2 = false;
        }
        

        if (intermediaire) {
            
            erruption.SetActive(false);
            intermediaire = false;
        }
        


    }
}
