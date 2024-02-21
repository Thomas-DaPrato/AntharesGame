using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commentateur : MonoBehaviour
{
    public List<AudioClip> commentateurAudioCoups;
    public List<AudioClip> commentateurAudioPiegesFlammes;
    public List<AudioClip> commentateurAudioPiegesGeyser;
    public List<AudioClip> commentateurAudioPiegesScie;
    public List<AudioClip> commentateurAudioPiegesLaser;


    public AudioSource audioSource;
    

    private bool isCommenting = false;
    private int r = 0;

    private float dureeAudio = 0;

    
    public void CommentateurCoups()
    {
        if (!isCommenting)
        {
            r = Random.Range(0, commentateurAudioCoups.Count - 1);
            AudioClip clip = commentateurAudioCoups[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;
            StartCoroutine(CanComment(dureeAudio));
        }
    }

    public void CommentateurPiege(string nomPiege)
    {
        if (!isCommenting)
        {

            if (nomPiege.Equals("Turret"))
            {
                r = Random.Range(0, commentateurAudioPiegesLaser.Count - 1);
                AudioClip clip = commentateurAudioPiegesLaser[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege.Equals("Flammes"))
            {
                r = Random.Range(0, commentateurAudioPiegesFlammes.Count - 1);
                AudioClip clip = commentateurAudioPiegesFlammes[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege.Equals("Saw"))
            {
                r = Random.Range(0, commentateurAudioPiegesScie.Count - 1);
                AudioClip clip = commentateurAudioPiegesScie[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege.Equals("Geyser"))
            {
                r = Random.Range(0, commentateurAudioPiegesGeyser.Count - 1);
                AudioClip clip = commentateurAudioPiegesGeyser[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }



        }
    }

    public IEnumerator CanComment(float durée)
    {
        yield return new WaitForSeconds(durée);


        isCommenting = false;
    }
}
