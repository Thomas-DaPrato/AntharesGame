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
    private float laserSize;
    public float knockbackForce = 20;
    public float damage = 10;

    public VisualEffect laserVFX;


    private void Start()
    {
        Vector3 distance = laserEnd.position - laserBegin.position;
        laserSize = distance.magnitude;
        StartCoroutine(Attentepiege(tempsCharge));
        attenteEnCours = false;

        
        if (rightTurret)
        {
            vitesse = -vitesse;
        }
    }

    private void FixedUpdate() {
        if (canRayCast) {
            bool raycastTouch = Physics.Raycast(laserBegin.position, laserEnd.position - laserBegin.position, out RaycastHit raycastHit, laserSize, playerMask);
            Debug.DrawRay(laserBegin.position, laserEnd.position - laserBegin.position, Color.red);
            if (raycastTouch) {
                Debug.Log("laser hit");
                canRayCast = false;
                raycastHit.transform.GetComponentInParent<PlayerController>().TakeDamage(damage, HitBox.HitBoxType.Trap);
                raycastHit.transform.GetComponentInParent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(rightTurret ? -1 : 1, 1));
                StartCoroutine(AttenteHit(0.2f));
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
                Debug.Log("sizeLaser " + laserSize);
                //laserVFX.SetFloat("SizeLaser", laserSize);
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
                canRayCast = false;
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
    IEnumerator AttenteHit(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);
        // Après l'attente, vous pouvez mettre votre code ici
        StopSound();
        laser.SetActive(false);



    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(laserBegin.position, laserEnd.position - laserBegin.position);
    }

}
