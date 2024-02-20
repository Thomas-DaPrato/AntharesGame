using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using VLB;
using DG.Tweening;

public class BaseFlammesMovement : MonoBehaviour
{

    [SerializeField] private int vitesse, tempsCharge;
    [SerializeField] private float limiteHaute, limiteBasse;
    [SerializeField]
    public MMF_Player flammesZonePlane;
    [SerializeField]
    public MMF_Player flammesZoneLight;
    [SerializeField]
    public VolumetricLightBeam redLight;
    [SerializeField]
    public MeshRenderer planeRenderer;

    [SerializeField]
    AudioSource son;

    [HideInInspector] public bool changeRound = false;


    public GameObject flammes, vib, VFXs;
    public AudioClip charge;
    private bool canMove = true;
    private bool attente = false;
    private int declancheur = 0;
    public bool flammeBouge = false;
    public float dureeCharge = 2;
    public float minValueIntensity = 96f;
    public float maxValueIntensity = 191f;




    private void Start()
    {

        StartCoroutine(Attentepiege(tempsCharge));
    }
    void Update()
    {
        if (!changeRound)
        {
            if (canMove)
            {

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
        if (!changeRound)
        {
            if (sec == dureeCharge)
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
                VFXs.SetActive(false);
                //flammesZonePlane.StopFeedbacks();
                planeRenderer.material.DOVector(new Vector4(minValueIntensity / 255, planeRenderer.material.GetColor("_EmissionColor").b, planeRenderer.material.GetColor("_EmissionColor").b, 1), "_EmissionColor", 1f);
            }


        }
    }

    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
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
        flammesZoneLight.StopFeedbacks();
        flammesZoneLight.RestoreInitialValues();
        VFXs.SetActive(false);
        StartCoroutine(AttenteRound(tempRound));
    }

    private void chargePiege(bool chargeOuPas)
    {
        if (chargeOuPas)
        {
            vib.SetActive(true);
            flammesZoneLight.PlayFeedbacks();
            planeRenderer.material.DOVector(new Vector4(maxValueIntensity, planeRenderer.material.GetColor("_EmissionColor").b, planeRenderer.material.GetColor("_EmissionColor").b, 1), "_EmissionColor", dureeCharge);
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
        VFXs.SetActive(true);
        flammesZoneLight.StopFeedbacks();
        flammesZoneLight.RestoreInitialValues();
    }


}


