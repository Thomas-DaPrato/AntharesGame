using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foule : MonoBehaviour
{

    public List<AudioClip> FouleAudioCoups;
    private bool isScreaming = false;
    private int r = 0;
    public AudioSource audioSource;
    private float dureeAudio = 0;
    public void FouleScream()
    {
        if (!isScreaming)
        {
            r = Random.Range(0, FouleAudioCoups.Count - 1);
            AudioClip clip = FouleAudioCoups[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isScreaming = true;
            StartCoroutine(CanScream(dureeAudio));
        }
    }

    public IEnumerator CanScream(float durée)
    {
        yield return new WaitForSeconds(durée);


        isScreaming = false;
    }

}
