using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;

public class GeyserBehaviour : MonoBehaviour
{
    [SerializeField] private int timeBetweenActivate;
    [SerializeField] private float tempsCharge;
    [SerializeField] private int declancheur=0;
    [SerializeField]
    public MMF_Player geyserZone;

    public float minValueG = 91;
    public float maxValueG = 255;
    
    public float minValueB = 191;
    public float maxValueB = 255;
    public float minValueR = 47;
    public float maxValueR = 0;

    public MeshRenderer neon;
    public MeshRenderer grille;


    [SerializeField]
    AudioSource son;
    public AudioClip charge,sonTir;
    public GameObject erruption,vib;
    private bool tremble,tir,intermediaire, intermediaire2 = false, changeRound=false;


    private void Start()
    {
        StartCoroutine(Attentepiege(timeBetweenActivate));
    }
    void Update()
    {
        //print("r " + neon.material.GetColor("_EmissionColor").r * 255 + " g " +  neon.material.GetColor("_EmissionColor").g * 255 + " b " +  neon.material.GetColor("_EmissionColor").b * 255);
        if (!changeRound) { 
            if (tremble)
            {
                intermediaire2 = true;

                //tremblement
                vib.SetActive(true);
                geyserZone.PlayFeedbacks();
                neon.material.DOVector(new Vector4(maxValueR/255f, maxValueG/255f, maxValueB/255f, 1), "_EmissionColor", 5);
                grille.material.DOVector(new Vector4(maxValueR/255f, maxValueG/255f, maxValueB/255f, 1), "_EmissionColor", 5);

                son.PlayOneShot(charge);
                StartCoroutine(AttenteCoroutine(tempsCharge));
                
                tremble = false;

            
            }
            else if (tir)
            {
                
                intermediaire = true;
                StartCoroutine(AttenteCoroutine(5f));
                erruption.SetActive(true);
                tir = false;
            
            }

        
            if (declancheur > timeBetweenActivate)
            {
                declancheur = 0;
                //Debug.Log("actif");
                print("ici");
                tremble = true;
            }

        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
        {
            declancheur = 0;
            if (intermediaire2)
            {
                //fin du tremblement
                son.Stop();
                Debug.Log("je joue le son");
                son.PlayOneShot(sonTir);
                geyserZone.StopFeedbacks();

                tir = true;
                intermediaire2 = false;
            }


            if (intermediaire)
            {
                erruption.SetActive(false);
                neon.material.DOVector(new Vector4(minValueR/255f, minValueG/255f, minValueB/255f, 1), "_EmissionColor", 1).OnComplete(() => geyserZone.RestoreInitialValues());
                grille.material.DOVector(new Vector4(minValueR/255f, minValueG/255f, minValueB/255f, 1), "_EmissionColor", 1).OnComplete(() => geyserZone.RestoreInitialValues());
                vib.SetActive(false);
                geyserZone.StopFeedbacks();
                intermediaire = false;
                son.Stop();
                StartCoroutine(Attentepiege(timeBetweenActivate));
            }

        }

    }


    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
            declancheur = timeBetweenActivate + 1;


    }
    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        changeRound = false;
        StartCoroutine(Attentepiege(timeBetweenActivate));
        


    }

    public void ChangeRound(float tempRound)
    {
        changeRound = true;
        vib.SetActive(false);
        geyserZone.StopFeedbacks();
        erruption.SetActive(false);
        intermediaire2 = false;
        changeRound = false;
        tir = false;
        StartCoroutine(AttenteRound(tempRound));
    }



}
