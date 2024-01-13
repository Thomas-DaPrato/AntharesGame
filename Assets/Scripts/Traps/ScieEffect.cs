using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ScieEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationScie;
    [SerializeField]
    AudioSource son;
    public int vitesse;
    public SphereCollider col;
    public AudioClip enMarche, touche;
    private bool changeRound = false;

    public VisualEffect VFXTouche;


    
    void Update()
    {
        if(!changeRound)
        transform.Rotate(rotationScie * vitesse * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!changeRound) { 
            if (other.gameObject.tag == "Player")
            {
                son.Pause();
                son.PlayOneShot(touche);
            
                VFXTouche.gameObject.transform.position = other.GetContact(0).point;
                VFXTouche.Play();
            
                other.gameObject.GetComponent<PlayerController>().TakeDamage(10.0f, HitBox.HitBoxType.Trap);
                other.gameObject.GetComponent<PlayerController>().ApplyKnockback(10, new Vector2(other.gameObject.GetComponent<PlayerController>().lastDirection * -1, 1));
                vitesse = -50;
                col.isTrigger = true;
                StartCoroutine(AttenteCoroutine(1f));
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !changeRound)
        {
            other.GetComponent<PlayerController>().ApplyKnockback(5, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));

        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {
        

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        if (!changeRound) {
            if (sec == 1)
            {
                vitesse = 0;
                StartCoroutine(AttenteCoroutine(1.1f));
            }
            else
            {
                son.UnPause();
                vitesse = 200;
                col.isTrigger = false;
            }
        }



    }


    
    IEnumerator AttenteRound(float sec)
    {

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Apr�s l'attente, vous pouvez mettre votre code ici
        changeRound = false;
        
        son.UnPause();


    }

    public void ChangeRound()
    {
        vitesse = 200;
        col.isTrigger = false;
        son.Pause();
        changeRound = true;
        StartCoroutine(AttenteRound(7f));
    }

}
