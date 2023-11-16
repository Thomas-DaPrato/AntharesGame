using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public int vitesse,reset;

    private void Start()
    {
        reset = 0;
    }
    void Update()
    {
        //Debug.Log(transform.position.y);
        if (transform.position.y < 1 && vitesse < 0)
        {
            Debug.Log("change vers haut");
            vitesse = -vitesse;
        }
        else if(transform.position.y > 14 && vitesse > 0)
        {
            vitesse = -vitesse;
            Debug.Log("change vers bas");
        }
        transform.Translate(Vector3.forward * Time.deltaTime * vitesse);
    }
}
