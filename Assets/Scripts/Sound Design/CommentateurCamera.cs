using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentateurCamera : MonoBehaviour
{
    public List<AudioClip> commentateurAudioCoups;
    public List<AudioClip> commentateurAudioPiegesFlammes;
    public List<AudioClip> commentateurAudioPiegesGeyser;
    public List<AudioClip> commentateurAudioPiegesScie;
    public List<AudioClip> commentateurAudioPiegesLaser;
    public List<AudioClip> commentateurMirrorMatch;
    public List<AudioClip> commentateurPresentationDiane;
    public List<AudioClip> commentateurPresentationCesar;
    public List<AudioClip> commentateurStart;
    public List<AudioClip> commentateurBienvenu;

    public AudioSource audioSource,audioSourceMusic;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject flammes;
    [SerializeField] private GameObject geyser;
    [SerializeField] private GameObject scie;
    [SerializeField] private GameObject laserD;
    [SerializeField] private GameObject laserG;

    private bool isCommenting = false;
    private int r=0;

    private float dureeAudio=0;

    private void Start()
    {

        r = Random.Range(0, commentateurBienvenu.Count - 1);
        AudioClip clip = commentateurBienvenu[r];
        dureeAudio = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        isCommenting = true;
        StartCoroutine(PresentationJoueur(dureeAudio));

        

    }

    private void Update()
    {
        if (!audioSourceMusic.isPlaying)
        {
            audioSourceMusic.Play();
        }
    }


    //il faut rajouter le piege.name comme parametre au moment de l'appel 
    public void CommentateurPiege(string nomPiege)
    {
        if (!isCommenting) {

            if(nomPiege==laserD.name|| nomPiege == laserG.name)
            {
                r = Random.Range(0, commentateurAudioPiegesLaser.Count - 1);
                AudioClip clip = commentateurAudioPiegesLaser[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege == flammes.name)
            {
                r = Random.Range(0, commentateurAudioPiegesFlammes.Count - 1);
                AudioClip clip = commentateurAudioPiegesFlammes[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege == scie.name)
            {
                r = Random.Range(0, commentateurAudioPiegesScie.Count - 1);
                AudioClip clip = commentateurAudioPiegesScie[r];
                dureeAudio = clip.length;
                audioSource.clip = clip;
                audioSource.Play();
                isCommenting = true;
                StartCoroutine(CanComment(dureeAudio));
            }
            else if (nomPiege == geyser.name)
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



    public IEnumerator CanComment(float durée)
    {
        yield return new WaitForSeconds(durée);


        isCommenting = false;
    }


    public IEnumerator CanCommentstart(float durée)
    {
        
        yield return new WaitForSeconds(durée);

        //pitch debut de match ready fight etc
        r = Random.Range(0, commentateurStart.Count - 1);
        AudioClip clip = commentateurStart[r];
        dureeAudio = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        isCommenting = true;
        StartCoroutine(musicCanPlay(dureeAudio));
    }


    public IEnumerator musicCanPlay(float durée)
    {
        yield return new WaitForSeconds(durée);

        audioSourceMusic.Play();
        isCommenting = false;
    }


    public IEnumerator CommentDiane(float durée)
    {

        yield return new WaitForSeconds(durée);


        r = Random.Range(0, commentateurPresentationDiane.Count - 1);
        AudioClip clip = commentateurPresentationDiane[r];
        dureeAudio = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        isCommenting = true;
        StartCoroutine(CanCommentstart(dureeAudio));
    }
    public IEnumerator CommentCesar(float durée)
    {

        yield return new WaitForSeconds(durée);


        r = Random.Range(0, commentateurPresentationCesar.Count - 1);
        AudioClip clip = commentateurPresentationCesar[r];
        dureeAudio = clip.length;
        audioSource.clip = clip;
        audioSource.Play();
        isCommenting = true;
        StartCoroutine(CanCommentstart(dureeAudio));
    }


    public IEnumerator PresentationJoueur(float durée)
    {

        yield return new WaitForSeconds(durée);


        if (player1.name == player2.name)
        {
            //mirror match

            r = Random.Range(0, commentateurMirrorMatch.Count - 1);
            AudioClip clip = commentateurMirrorMatch[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;
            StartCoroutine(CanCommentstart(dureeAudio));
        }
        else if (player1.name == "Diane")
        {   //joueur1 Dianne et joueur2 Cesar


            //presentation du joueur 1

            r = Random.Range(0, commentateurPresentationDiane.Count - 1);
            AudioClip clip = commentateurPresentationDiane[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;


            //presentation du joueur 2

            StartCoroutine(CommentCesar(dureeAudio));
        }
        else
        {   //joueur1 Cesar et joueur2 Dianne 


            //presentation du joueur 1

            r = Random.Range(0, commentateurPresentationCesar.Count - 1);
            AudioClip clip = commentateurPresentationCesar[r];
            dureeAudio = clip.length;
            audioSource.clip = clip;
            audioSource.Play();
            isCommenting = true;


            //presentation du joueur 2

            StartCoroutine(CommentDiane(dureeAudio));
        }
    }

}
