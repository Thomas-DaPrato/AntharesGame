using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField]
    private GameObject laserD;
    [SerializeField]
    private GameObject laserG;
    [SerializeField]
    private GameObject flammesBas;
    [SerializeField]
    private GameObject flammesHaut;
    [SerializeField]
    private GameObject geyserD;
    [SerializeField]
    private GameObject geyserG;
    [SerializeField]
    private GameObject scieD;
    [SerializeField]
    private GameObject scieG;

    public float tempsCharge = 7f;

    private Transform lD, lG, fB, fH;
    private bool flammemove1, flammemove2;
    private void Start()
    {



        flammemove1 = flammesBas.GetComponent<BaseFlammesMovement>().flammeBouge;
        flammemove2 = flammesHaut.GetComponent<BaseFlammesMovement>().flammeBouge;



        if (flammemove1)
        {
            fB = flammesBas.transform;
        }
        if (flammemove2)
        {
            fH = flammesHaut.transform;
        }


    }

    public void ResetTrap()
    {


        if (flammemove1)
        {
            flammesBas.transform.position = fB.position;
        }
        if (flammemove2)
        {
            flammesHaut.transform.position = fH.position;
        }

        flammesBas.GetComponent<BaseFlammesMovement>().ChangeRound(tempsCharge);
        flammesHaut.GetComponent<BaseFlammesMovement>().ChangeRound(tempsCharge);

        laserD.GetComponent<CanonMouvement>().ChangeRound(tempsCharge);
        laserG.GetComponent<CanonMouvement>().ChangeRound(tempsCharge);

        geyserD.GetComponent<GeyserBehaviour>().ChangeRound(tempsCharge);
        geyserG.GetComponent<GeyserBehaviour>().ChangeRound(tempsCharge);

        scieD.GetComponent<ScieEffect>().ChangeRound(tempsCharge);
        scieG.GetComponent<ScieEffect>().ChangeRound(tempsCharge);


    }



}
