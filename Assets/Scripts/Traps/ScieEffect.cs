using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationScie;
    public int vitesse;
    public SphereCollider col;


    void Update()
    {
        transform.Rotate(rotationScie * vitesse * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(10.0f, HitBox.HitBoxType.Trap);
            other.GetComponent<PlayerController>().ApplyKnockback(10, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));
            vitesse = -50;
            col.isTrigger = false;
            StartCoroutine(AttenteCoroutine(1f));
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().ApplyKnockback(5, new Vector2(other.GetComponent<PlayerController>().lastDirection * -1, 1));

        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {
        

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        if (sec == 1)
        {
            vitesse = 0;
            StartCoroutine(AttenteCoroutine(1.1f));
        }
        else
        {
            vitesse = 300;
            col.isTrigger = true;
        }
        

        

    }

}
