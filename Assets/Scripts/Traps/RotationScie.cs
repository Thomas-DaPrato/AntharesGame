using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RotationScie : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationScie;
    [SerializeField]
    AudioSource son;
    public GameObject scie,vfxScie;
    private int vitesse;
    public float knockbackForce=10;
    public float damage = 10;
    public int vitRotation;
    public CapsuleCollider col;
    public AudioClip enMarche, touche;
    private bool changeRound = false;

    public VisualEffect VFXTouche;

    private void Start()
    {
        
        vitesse = vitRotation;
    }

    void Update()
    {
       

        if (!changeRound)
            scie.transform.Rotate(rotationScie * vitesse * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!changeRound)
        {
            if (other.gameObject.tag == "Player" && vitesse == vitRotation)
            {
                son.Pause();
                vfxScie.SetActive(false);
                son.PlayOneShot(touche);

                VFXTouche.gameObject.transform.position = other.GetContact(0).point;
                VFXTouche.Play();

                other.gameObject.GetComponent<PlayerController>().TakeDamage(damage, HitBox.HitBoxType.Trap);
                other.gameObject.GetComponent<PlayerController>().ApplyKnockback(knockbackForce, new Vector2(other.gameObject.GetComponent<PlayerController>().lastDirection * -1, 1));
                vitesse = -50;

                StartCoroutine(AttenteCoroutine(1f));
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
            if (sec == 1)
            {
                vitesse = 0;
                StartCoroutine(AttenteCoroutine(1.1f));
            }
            else
            {
                son.UnPause();
                vfxScie.SetActive(true);
                vitesse = vitRotation;
                
            }
        }



    }



    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        changeRound = false;
        vitesse = vitRotation;

        son.UnPause();


    }

    public void ChangeRound(float tempRound)
    {
        vitesse = 0;
        col.isTrigger = false;
        son.Pause();
        changeRound = true;
        StartCoroutine(AttenteRound(tempRound));
    }

}
