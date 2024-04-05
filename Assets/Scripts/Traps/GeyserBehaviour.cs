using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using DG.Tweening;

public class GeyserBehaviour : MonoBehaviour
{
    [SerializeField] private int timeBetweenActivate;
    [SerializeField] private float tempsCharge;
    [SerializeField] private int declancheur = 0;
    [SerializeField]
    public MMF_Player geyserZone;

    public float minValueR = 47;
    public float maxValueR = 0;

    public float minValueG = 91;
    public float maxValueG = 255;

    public float minValueB = 191;
    public float maxValueB = 255;


    public MeshRenderer neon;
    public MeshRenderer grille;


    public MMF_Player geyserSFX;

    public GameObject erruption, vib;
    private bool tremble, tir, intermediaire, intermediaire2 = false, changeRound = false;


    private void Start()
    {
        StartCoroutine(Attentepiege(timeBetweenActivate));
    }
    void Update()
    {
        //print("r " + neon.material.GetColor("_EmissionColor").r * 255 + " g " +  neon.material.GetColor("_EmissionColor").g * 255 + " b " +  neon.material.GetColor("_EmissionColor").b * 255);
        if (!changeRound)
        {
            if (tremble)
            {
                intermediaire2 = true;

                //tremblement
                vib.SetActive(true);
                geyserZone.PlayFeedbacks();
                neon.material.DOVector(new Vector4(maxValueR, maxValueG, maxValueB, 1), "_EmissionColor", 5).SetId("Lux");
                grille.material.DOVector(new Vector4(maxValueR, maxValueG, maxValueB, 1), "_EmissionColor", 5).SetId("Lux");

                geyserSFX.PlayFeedbacks();
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


                tremble = true;
            }

        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);


        if (!changeRound)
        {
            declancheur = 0;
            if (intermediaire2)
            {
                //fin du tremblement

                geyserZone.StopFeedbacks();

                tir = true;
                intermediaire2 = false;
            }


            if (intermediaire)
            {
                neon.material.DOVector(new Vector4(minValueR / 255f, minValueG / 255f, minValueB / 255F, 1), "_EmissionColor", 1).SetId("Lux");//.OnComplete(() => geyserZone.RestoreInitialValues());
                grille.material.DOVector(new Vector4(minValueR / 255f, minValueG / 255f, minValueB / 225f, 1), "_EmissionColor", 1).SetId("Lux");//.OnComplete(() => geyserZone.RestoreInitialValues());
                StartCoroutine(WaitToDesactivateCollision(1));
                StartCoroutine(WaitToDesactivate(2));
                geyserZone.StopFeedbacks();
                intermediaire = false;

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

    IEnumerator WaitToDesactivate(float sec)
    {
        yield return new WaitForSeconds(sec);
        vib.SetActive(false);
    }
    IEnumerator WaitToDesactivateCollision(float sec)
    {
        yield return new WaitForSeconds(sec);
        erruption.SetActive(false);
    }

    public void ResetColor()
    {
        DOTween.Kill("Lux");
        neon.material.SetVector("_EmissionColor", new Vector4(minValueR / 255f, minValueG / 255f, minValueB / 255F, 1));
        grille.material.SetVector("_EmissionColor", new Vector4(minValueR / 255f, minValueG / 255f, minValueB / 255F, 1));
    }



}
