using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentateurCamera : MonoBehaviour
{
    public List<AudioClip> commentateurAudioCoups;
    public List<AudioClip> commentateurAudioPieges;
    public List<AudioClip> commentateurStart;

    public AudioSource audioSource;
    private bool isCommenting = false;

    private float dureeAudio=0;

    private void Start()
    {
        
        if (!isCommenting)
        {
            int r = Random.Range(0, commentateurStart.Count);
            AudioClip clip = commentateurStart[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;
            StartCoroutine(CanComment(dureeAudio));
        }

    }

    public void CommentateurPiege()
    {
        if (!isCommenting) { 
        int r = Random.Range(0, commentateurAudioPieges.Count);
        AudioClip clip = commentateurAudioPieges[r];
        dureeAudio = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        isCommenting = true;
        StartCoroutine(CanComment(dureeAudio));
        }
    }
    public void CommentateurCoups()
    {
        if (!isCommenting)
        {
            int r = Random.Range(0, commentateurAudioCoups.Count);
            AudioClip clip = commentateurAudioCoups[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;
            StartCoroutine(CanComment(dureeAudio));
        }
    }

    public IEnumerator CanComment(float durée)
    {
        yield return new WaitForSeconds(durée);
        isCommenting = false;
    }

}
