using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class CanonMouvement : MonoBehaviour
{
    
    [SerializeField] private int vitesse, tempsCharge;
    [SerializeField] private Transform limiteHaute, limiteBasse, laserBegin, laserEnd;
    [SerializeField] private bool tir = false;
    [SerializeField] private bool canMove = true;
    [SerializeField]
    AudioSource son;
    public AudioClip chargement,sonTir;
    public GameObject turret;
    public GameObject laser,charge;
    public LayerMask playerMask;
    private int declancheur = 0;
    public bool rightTurret = false;
    private bool attenteEnCours = false;
    private bool changeRound = false;
    private bool canRayCast = false;

    public VisualEffect laserVFX;


    private void Start()
    {
        StartCoroutine(Attentepiege(tempsCharge));
        attenteEnCours = false;

        
        if (rightTurret)
        {
            vitesse = -vitesse;
        }
    }

    private void FixedUpdate() {
        if (canRayCast) {
            if (Physics.Raycast(laserBegin.position, laserEnd.position, out RaycastHit raycastHit, Mathf.Abs(laserEnd.position.x - laserBegin.position.x), playerMask)) {
                Debug.Log("laser hit");
                canRayCast = false;
                raycastHit.transform.GetComponentInParent<PlayerController>().TakeDamage(10.0f, HitBox.HitBoxType.Trap);
                raycastHit.transform.GetComponentInParent<PlayerController>().ApplyKnockback(20, new Vector2(raycastHit.transform.GetComponentInParent<PlayerController>().lastDirection * -1, 1));
                StopSound();
                laser.SetActive(false);
            }
        }
            
    }
    void Update()
    {
        if (!changeRound) { 
            if (tir)
            {
                if (canMove)
                {
                    canMove = false;
                    charge.SetActive(true);
                    son.PlayOneShot(chargement);
                    StartCoroutine(AttenteCoroutine(2.3f));
                }




            }
            else
            {
                if (turret.transform.position.y > limiteHaute.position.y && vitesse<0)
                {
                    vitesse = -vitesse;
                }
                else if (turret.transform.position.y < limiteBasse.position.y && vitesse>0)
                {
                    vitesse = -vitesse;
                }
                turret.transform.Translate(Vector3.forward * Time.deltaTime * vitesse);


                
                if (declancheur > tempsCharge)
                {
                    declancheur = 0;
                    tir = true;
                }

            }
        }

    }

    public void StopSound()
    {
        son.Stop();
    }

    IEnumerator AttenteCoroutine(float sec)
    {
        if (!changeRound) { 
            attenteEnCours = true;

            // Attendez pendant x secondes
            yield return new WaitForSeconds(sec);

            // Après l'attente, vous pouvez mettre votre code ici
            if (sec == 2.3f && !changeRound)
            {
                charge.SetActive(false);
                laser.SetActive(true);
                laserVFX.SetFloat("SizeLaser", Mathf.Abs(laserEnd.position.x - laserBegin.position.x));
                canRayCast = true;
                son.PlayOneShot(sonTir);
                StartCoroutine(AttenteCoroutine(3f));
            }
            else if (!changeRound)
            {
                laser.SetActive(false);
                declancheur = 0;
                tir = false;
                canMove = true;
                StartCoroutine(Attentepiege(tempsCharge));

                attenteEnCours = false;
            }

        }
    }



    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        changeRound = false;        
        declancheur = 0;
        canMove = true;
        attenteEnCours = false;



    }

    public void ChangeRound(float tempRound)
    {
        changeRound = true;
        declancheur = 0;
        tir = false;
        laser.SetActive(false);
        StartCoroutine(AttenteRound(tempRound));
    }
    IEnumerator Attentepiege(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (!changeRound)
            declancheur = tempsCharge + 1;



    }

}
