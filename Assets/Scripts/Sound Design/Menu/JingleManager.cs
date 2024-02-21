using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class JingleManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource jingle;
    public AudioClip soundJingle;
    public GameObject jinglemanager;
    public GameObject sound;
    public void playSound()
    {
        sound.GetComponent<MMSoundManager>().PauseAllSounds();   
        jingle.PlayOneShot(soundJingle);
        DontDestroyOnLoad(jinglemanager);
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name.Equals("Game_Final"))
        {
            jingle.Stop();
            Destroy(jinglemanager);
        }
    }

}
