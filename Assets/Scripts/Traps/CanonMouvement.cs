using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMouvement : MonoBehaviour
{
    
    [SerializeField] private int vitesse;
    [SerializeField] private float limiteHaute,limiteBasse;
    [SerializeField] private bool tir = false;
    public GameObject laser;
    private int declancheur = 0;
    private bool attenteEnCours;


    private void Start()
    {
        attenteEnCours = false;
        if (this.transform.position.x < 0)
        {
            vitesse = -vitesse;
        }
    }
    void Update()
    {


        if (tir)
        {

            laser.SetActive(true);
            StartCoroutine(AttenteDeCinqSecondesCoroutine());


        }
        else
        {

            if (transform.position.y > limiteHaute && vitesse>0)
            {

                vitesse = -vitesse;
            }
            else if (transform.position.y < limiteBasse && vitesse<0)
            {

                vitesse = -vitesse;
            }
            transform.Translate(Vector3.up * Time.deltaTime*vitesse);
            

            declancheur++;
            if (declancheur > 1200)
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
