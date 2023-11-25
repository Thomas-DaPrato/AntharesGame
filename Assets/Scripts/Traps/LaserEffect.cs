using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    public GameObject laser;
    public GameObject canon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(10.0f, HitBox.HitBoxType.Trap);
            other.GetComponent<PlayerController>().ApplyKnockback(20, new Vector2(other.GetComponent<PlayerController>().lastDirection*-1,1));
            StartCoroutine(AttenteCoroutine(0.1f));
        }
    }

    IEnumerator AttenteCoroutine(float sec)
    {
        

        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);

        // Après l'attente, vous pouvez mettre votre code ici
        laser.SetActive(false);
        canon.GetComponent<CanonMouvement>().stopSound();


    }
}
