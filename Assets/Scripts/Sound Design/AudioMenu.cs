using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public List<AudioClip> commentateurBienvenu;
    public List<AudioClip> fouleReact;
    public AudioSource audioSource;
    public AudioSource audioSourceFoule;
    public float timeBetweenCom;
    private int r;
    private AudioClip clip;
    private int rFoule;
    private AudioClip clipFoule;


    private void Start()
    {
        StartCoroutine(WaitCom(timeBetweenCom));
        StartCoroutine(WaitFoule(1f));
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

    IEnumerator WaitFoule(float sec)
    {
        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);



        rFoule = Random.Range(0, fouleReact.Count - 1);
        clipFoule = fouleReact[rFoule];
        float dureeAudio = clipFoule.length;
        audioSourceFoule.clip = clipFoule;
        audioSourceFoule.Play();
        StartCoroutine(WaitCom(dureeAudio));

    }



}
