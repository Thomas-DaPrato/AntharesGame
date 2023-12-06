using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentateurCamera : MonoBehaviour
{
    public List<AudioClip> commentateurAudioCoups;
    public List<AudioClip> commentateurAudioPieges;
    public AudioSource audioSource;
    private bool isCommenting = false;
    


    public void CommentateurPiege()
    {
        if (!isCommenting) { 
        int r = Random.Range(0, commentateurAudioPieges.Count);
        AudioClip clip = commentateurAudioPieges[r];
        audioSource.clip = clip;
        audioSource.Play();
            isCommenting = true;
            //arret
        }
    }
    public void CommentateurCoups()
    {
        if (!isCommenting)
        {
            int r = Random.Range(0, commentateurAudioCoups.Count);
            AudioClip clip = commentateurAudioCoups[r];
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;
            //arret
        }
    }

}
