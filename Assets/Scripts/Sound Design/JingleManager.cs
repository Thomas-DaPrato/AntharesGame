using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JingleManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource jingle;
    public AudioClip soundJingle;
    public GameObject jinglemanager;
    public void playSound()
    {
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
