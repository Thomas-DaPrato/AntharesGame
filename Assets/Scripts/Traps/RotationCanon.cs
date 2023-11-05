using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCanon : MonoBehaviour
{

    [SerializeField] private Vector3 rotationDuCanon;
    [SerializeField] private int vitesse,tempsCharge;
    [SerializeField] private bool tir = false;
    public GameObject laser;
    private int declancheur = 0;
    private bool attenteEnCours = false;


    void Update()
    {
        if (tir)
        {

            laser.SetActive(true);
            StartCoroutine(AttenteDeCinqSecondesCoroutine());
            
            
        }
        else {

            if (transform.rotation.z >= 0.5 || transform.rotation.z <= -0.5)
            {
                vitesse = -vitesse;
               
            }
                transform.Rotate(rotationDuCanon * vitesse * Time.deltaTime);

                declancheur++;
                if (declancheur > tempsCharge)
                {
                    Debug.Log("actif");
                    tir = true;
                }

        }

         


    }

    IEnumerator AttenteDeCinqSecondesCoroutine()
    {
        attenteEnCours = true;

        // Attendez pendant 5 secondes
        yield return new WaitForSeconds(3f);

        // Après l'attente, vous pouvez mettre votre code ici
        declancheur = 0;
        tir = false;
        laser.SetActive(false);

        attenteEnCours = false;
    }
}
