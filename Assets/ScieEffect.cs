using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieEffect : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationScie;
    public int vitesse;


    void Update()
    {
        transform.Rotate(rotationScie * vitesse * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(1);
            other.GetComponent<PlayerController>().ApplyKnockback(20, other.GetComponent<PlayerController>().lastDirection * -1);

        }
    }

}
