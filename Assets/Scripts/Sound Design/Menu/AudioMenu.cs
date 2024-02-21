using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public List<AudioClip> commentateurBienvenu;
    public List<AudioClip> commentateur;
    public List<AudioClip> fouleReact;
    public AudioSource audioSource;
    public AudioSource audioSourceFoule;
    public AudioSource audioSourceAStop;
    public AudioSource audioSourceAStop2;
    public float timeBetweenCom;
    
    
    private int rFoule;
    private AudioClip clipFoule;


    private void Start()
    {
        int r = Random.Range(0, commentateurBienvenu.Count - 1);
        AudioClip clip = commentateurBienvenu[r];

        audioSource.clip = clip;
        audioSource.Play();
        StartCoroutine(WaitCom(timeBetweenCom));
        StartCoroutine(WaitFoule(0.1f));
    }

    IEnumerator WaitCom(float sec)
    {
        // Attendez pendant x secondes
        yield return new WaitForSeconds(sec);



        int r = Random.Range(0, commentateur.Count-1);
        AudioClip clip = commentateur[r];
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
        StartCoroutine(WaitFoule(dureeAudio));

    }

    public void StopMusic()
    {
        audioSource.Pause();
        audioSourceFoule.Pause();
        audioSourceAStop.Pause();
        audioSourceAStop2.Pause();
    }



}
