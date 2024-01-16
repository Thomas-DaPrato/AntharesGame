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

    private Transform lD, lG,fB,fH;
    private bool flammemove1, flammemove2;
    private void Start()
    {
        flammemove1 = flammesBas.GetComponent<BaseFlammesMovement>().mouvementPermis;
        flammemove2 = flammesHaut.GetComponent<BaseFlammesMovement>().mouvementPermis;

        lD = laserD.transform;
        lG = laserG.transform;

        if (flammemove1) {
            fB = flammesBas.transform;
        }
        if (flammemove2)
        {
            fH = flammesHaut.transform;
        }

    }

    public void ResetTrap()
    {
        laserD.transform.position = lD.position;
        laserG.transform.position = lG.position;

        if (flammemove1)
        {
            flammesBas.transform.position = fB.position;
        }
        if (flammemove2)
        {
            flammesHaut.transform.position = fH.position;
        }

        flammesBas.GetComponent<BaseFlammesMovement>().ChangeRound();
        flammesHaut.GetComponent<BaseFlammesMovement>().ChangeRound();

        laserD.GetComponent<CanonMouvement>().ChangeRound();
        laserG.GetComponent<CanonMouvement>().ChangeRound();

        geyserD.GetComponent<GeyserBehaviour>().ChangeRound();
        geyserG.GetComponent<GeyserBehaviour>().ChangeRound();

        scieD.GetComponent<ScieEffect>().ChangeRound();
        scieG.GetComponent<ScieEffect>().ChangeRound();


    }



}
