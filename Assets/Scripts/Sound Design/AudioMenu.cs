using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public List<AudioClip> commentateurBienvenu;
    public AudioSource audioSource;
    public int timeBetweenCom;
    private int r;
    private AudioClip clip;


    private void Start()
    {
        StartCoroutine(WaitCom(timeBetweenCom));
    }

    IEnumerator WaitCom(float sec)
    {
        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);



        r = Random.Range(0, commentateurBienvenu.Count - 1);
        clip = commentateurBienvenu[r];
        
        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(WaitCom(timeBetweenCom));

    }



}
